using System;
using System.Collections.Generic;
using data;
using game.manager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace other
{
    [Flags]
    public enum TileMapType
    {
        None = 0,
        //基础图层
        Base_Layer = 1 << 0,
        //沼泽图层
        Swamp_Layer = 1 << 1,
        //不能通过 且 不能造塔
        BlockPlayer_Layer = 1 << 2
        
    }
    
    [Serializable]
    public class TileMapData
    {
        [ShowInInspector]
        [ReadOnly]
        public TileMapType _tileMapType;
        [LabelWidth(100)]
        public int index;
        [LabelWidth(100)]
        public bool hadCollider;
        [LabelWidth(100)]
        public bool isTrigger;
        public TileMapData(TileMapType tileMapType)
        {
            _tileMapType = tileMapType;
        }
    }
    [Serializable]
    public class TileMapLayerDic : SerializableDic<TileMapType, TileMapData>
    {
        
    }
    [CreateAssetMenu(fileName = "TileMapData", menuName = "TileMapData")]
    public class TileMapSetting :ScriptableObject
    {
        
        [AssetsOnly]
        [ShowInInspector]
        private TileBase outLineTile;
        [AssetsOnly]
        public TileBase waterTile;
        [AssetsOnly] 
        public TileBase buildBaseTile;

        public bool CanSetHighlight = true;
        public Color darkColor = new Color(0.3f, 0.3f, 0.3f, 1);
        [ShowInInspector]
        public TileMapType PlayerCantWalk =  TileMapType.BlockPlayer_Layer;

        public TileMapLayerDic tileMapLayerDic = new TileMapLayerDic() { };
        
        
        private static TileMapType highLight = TileMapType.None;
        private static TileMapType noLayerType => TileMapType.None;
        
        
        public static void DeleteAllTileMap()
        {
            Transform map = MapManager.Instance.transform;
            for (int i = map.childCount-1; i >=0; i--)
            {
                GameObject.DestroyImmediate(map.GetChild(i).gameObject);
            }
        }
        
        public void CreateAllTileMap()
        {
            foreach (TileMapType tileMapType in Enum.GetValues(typeof(TileMapType)))
            {
                if (noLayerType.HadFlag(tileMapType))
                {
                    continue;
                }
                GetTileMap(tileMapType);
            }
        }

        
        //获取tilemap
        public Tilemap GetTileMap(TileMapType type)
        {
            if (!type.RealType())
                return null;
            if (!tileMapLayerDic.ContainsKey(type))
                return null;
            Transform map = MapManager.Instance.transform;
            TileMapData tileMapData = tileMapLayerDic[type];
            // Transform tileMap = map.GetChild(type.FlagIndex()-1);
            int index = type.FlagIndex() - 1;
            if (map.childCount > index)
                return map.GetChild(index).GetComponent<Tilemap>();
            
            var tileMap = new GameObject($"TileMap_{index}_{type}").transform;
            tileMap.SetParent(map);
            tileMap.localPosition = Vector3.zero;
            tileMap.localScale = Vector3.one;
            tileMap.localRotation = Quaternion.identity;
            
            TilemapRenderer rend = tileMap.gameObject.AddComponent<TilemapRenderer>();
            rend.sortingLayerName = "TileMap";
            rend.sortingOrder = tileMapData.index;
            // if (ColliderLayer.HasFlag(type))
            if(tileMapData.hadCollider)  
            {
                tileMap.gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                tileMap.gameObject.AddComponent<CompositeCollider2D>().isTrigger = !PlayerCantWalk.HadFlag(type);
                tileMap.gameObject.AddComponent<TilemapCollider2D>().usedByComposite = tileMapData.hadCollider;
                tileMap.gameObject.layer = LayerMask.NameToLayer("Map");
            }
            return tileMap.GetComponent<Tilemap>();
        }
        
        public TileMapType GetTileMapType(GameObject tileMapObj)
        {
            return Enum.TryParse(tileMapObj.name, out TileMapType type) ? type : TileMapType.None;
        }

        public TileBase GetTileBase(TileMapType type,Vector3Int pos)
        {
            Tilemap tileMap = GetTileMap(type);
            if (tileMap == null)
                return null;
            return tileMap.GetTile(pos);
        }
      
        public void CheckSelect(GameObject select)
        {
            if (select == null || select.transform.parent == null || select.transform.parent != MapManager.Instance.transform)
            {
                HighLightMap(TileMapType.None);
                return;
            }
            HighLightMap(GetTileMapType(select.transform.gameObject));
        }
        
        public void CheckSelect(TileMapType select)
        {
            HighLightMap(select);
        }

        private void HighLightMap(TileMapType tileMapType)
        {
            if(highLight == tileMapType || !CanSetHighlight)
                return;
            highLight = tileMapType;
            foreach (TileMapType type in Enum.GetValues(typeof(TileMapType)))
            {
                if(type == TileMapType.None)
                    continue;
                Tilemap tilemap = GetTileMap(type);
                if(tilemap == null)
                    continue;
                if (!tileMapType.RealType() || type == tileMapType)
                {
                    tilemap.color = Color.white;
                }
                else
                {
                    tilemap.color = darkColor;
                }
            }
        }
        
        public void ShowOutline(TileMapType tileMapType)
        {
            if (!tileMapType.RealType())
            {
                Debug.Log(tileMapType);
                HideOutline();
                return;
            }
            ShowOutline(GetTileMap(tileMapType));
        }
        
        public void ShowOutline(Tilemap tilemap)
        {
            if(tilemap == null)
                return;
            TileBase temp = outLineTile;
            Debug.Log(temp == null);
            Transform map = tilemap.transform.parent;
            Transform outline = map.transform.Find("Outline");
            if (outline == null)
            {
                outline = GameObject.Instantiate(tilemap.gameObject, map).transform;
                outline.GetComponent<TilemapRenderer>().sortingLayerName = "Ui";
                outline.name = "Outline";
            }
            else
            {
                outline.gameObject.SetActive(true);
            }

            Tilemap outlineTilemap = outline.GetComponent<Tilemap>();
            SetOutline(tilemap, outlineTilemap);
        }

        private void SetOutline(Tilemap baseMap, Tilemap outlineMap)
        {
            var allTile = GetAllTilePoint(baseMap);
            outlineMap.ClearAllTiles();
            foreach (var current in allTile)
            {
                outlineMap.SetTile(current, outLineTile);
                // CheckDir(PointDir.Up,current, checkedTile, outlineMap);
                // CheckDir(PointDir.Down,current, checkedTile, outlineMap);
                // CheckDir(PointDir.Left,current, checkedTile, outlineMap);
                // CheckDir(PointDir.Right,current, checkedTile, outlineMap);
            }
        }

      
        
        public static void HideOutline()
        {
            Transform map = MapManager.Instance.transform;
            GameObject outline = map.transform.Find("Outline").gameObject;
            if (outline != null)
            {
                outline.SetActive(false);
            }
        }
        
        public static List<Vector3Int> GetAllTilePoint(Tilemap tilemap)
        {
            List<Vector3Int> list = new List<Vector3Int>();
            BoundsInt bounds = tilemap.cellBounds;
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    if (tilemap.HasTile(pos))
                    {
                        list.Add(pos);
                    }
                }
            }
            return list;
        }
    }
    
}
