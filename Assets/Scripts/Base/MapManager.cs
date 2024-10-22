using System.Collections.Generic;
using other;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace game
{
    public enum PointDir
    {
        Up = 0,
        RUp = 1,
        Right = 2,
        RDown = 3,
        Down = 4,
        LDown = 5,
        Left = 6,
        LUp = 7
    }

    
    public class MapManager : EditorAction<MapManager>
    {
        public TileMapType showMap;
        public static Vector3 mapOffset;
        public static bool mapLoadComplete { get;private set; }
        private Dictionary<string, TileBase> localTileDic;
        
        protected override void Init()
        {
            base.Init();
            mapLoadComplete = false;
        }


        #region LoadMap
      
        
        #endregion

    }

}
