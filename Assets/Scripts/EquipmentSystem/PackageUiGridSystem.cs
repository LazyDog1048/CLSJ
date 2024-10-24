using System.Collections.Generic;
using data;
using EquipmentSystem;
using game;
using tool;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GridSystem
{
    public class PackageUiGridSystem : MonoBehaviour,IPlayerPackageSlot,IPointerEnterHandler,IPointerExitHandler
    {
        public static bool isPlayerGrid = false;
        [SerializeField]
        private RectTransform gridParent;
        [SerializeField]
        public RectTransform itemParent;
        [SerializeField]
        private Transform oriGridCell;
        [SerializeField]
        private UiPackageItem uiItemOri;   
        public int gridWidth = 20;
        public int gridHeight = 10;
        public float cellSize = 10;
        
        private UiGrid<UiGridObject> grid;
        public UiGrid<UiGridObject> Grid => grid;
        
        public List<UiPackageItem> boxItemDataList { get; set; }
        
        public static Vector2 CellSize = new Vector2(11, 11);
        public void Init()
        {
            Vector3 offset = gridParent.position;
            gridParent.sizeDelta = new Vector2(gridWidth, gridHeight) * cellSize;
            grid = new UiGrid<UiGridObject>(gridWidth,gridHeight,cellSize/13.333f,offset,NewBuildingObject);
            AddCell(oriGridCell,gridParent);
            boxItemDataList = new List<UiPackageItem>();
        }


        private UiGridObject NewBuildingObject(Grid<UiGridObject> grid,int x,int y)
        {
            return new UiGridObject(grid,x,y);
        }
        
        private void AddCell(Transform ori,RectTransform parent)
        {
            grid.ForEach((cell) =>
            {
                Transform newT = Instantiate(ori, parent);
                cell.InitGameObject(newT);
            });
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isPlayerGrid = GetType() == typeof(PlayerPackageUiGridSystem);
            PackageItemPreview.Instance.SwitchSlot(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GameManager.Instance.FrameEndExecute(() =>
            {
                if (!UiPackageItem.InDetailPanel)
                {
                    PackageItemPreview.Instance.RemoveSlot();
                }
            });
        }

        public virtual void PutDownItem(UiPackageItem item)
        {
            if (item.CheckAddToItem(grid))
            {
                
                PackageItemPreview.Instance.ClearItem();
                return;
            }
            if (item.CheckCanPutOnGrid(grid))
            {
                item.PutOnGrid(this);
                boxItemDataList.Add(item);
                PackageItemPreview.Instance.ClearItem();
            }
        }
        
        public virtual void PickUpItem()
        {
            var mousePos = GetMousePos.GetMousePosition();
            UiGridObject uiGridObject = grid.GetGridObject(mousePos);
            
            if (!ItemFunctionPanel.isActive && uiGridObject != null && uiGridObject.UiPackageItem != null)
            {
                var item = uiGridObject.UiPackageItem;
                item.PickOnGrid(grid);
                boxItemDataList.Remove(item);
                PackageItemPreview.Instance.SetPackageItem(item);
            }
        }
        
        public void ClearItem()
        {
            foreach (var uiGridObject in grid.GridArray)
            {
                if (uiGridObject.HasItem)
                {
                    var item = uiGridObject.UiPackageItem;
                    uiGridObject.UiPackageItem.PickOnGrid(grid);
                    GameObject.DestroyImmediate(item.gameObject);
                }
            }
            boxItemDataList.Clear();
        }


        public void AddItem(PackageItemData packageItemData)
        {
            UiPackageItem uiPackageItem = Instantiate(uiItemOri);
            uiPackageItem.InitItem(this,packageItemData);            
            boxItemDataList.Add(uiPackageItem);
        }

        public Vector2Int FindEnablePoint(Shape_Data shapeData)
        {
            
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    UiGridObject obj = grid.GetGridObject(x, y);
                    if(obj.HasItem)
                        continue;
                    if(CheckShape(obj,shapeData))
                        return new Vector2Int(x,y);
                }
            }
            return new Vector2Int(-1,-1);
        }

        private bool CheckShape(UiGridObject start,Shape_Data shapeData)
        {
            foreach (var shapePoint in shapeData.points)
            {
                int x = start.X + shapePoint.x;
                int y = start.Y + shapePoint.y;
                UiGridObject obj = grid.GetGridObject(x, y);
                if (obj == null || obj.HasItem)
                    return false;
            }
            return true;
        }
        
        
    }
    
}
