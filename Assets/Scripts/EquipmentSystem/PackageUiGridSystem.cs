using System.Collections.Generic;
using data;
using EquipmentSystem;
using tool;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GridSystem
{
    public class PackageUiGridSystem : MonoBehaviour,IPlayerPackageSlot,IPointerEnterHandler,IPointerExitHandler
    {
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
        private List<UiGridObject> uiGridObjects;
        public void Init()
        {
            Vector3 offset = gridParent.position;
            gridParent.sizeDelta = Package_Panel.CellSize * new Vector2(gridWidth, gridHeight) * cellSize;
            grid = new UiGrid<UiGridObject>(gridWidth,gridHeight,cellSize,offset,NewBuildingObject);
            AddCell(oriGridCell,gridParent);
            uiGridObjects = new List<UiGridObject>();
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
            PackageItemPreview.Instance.SwitchGrid(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }

        // private void UpdateItemEnable()
        // {
        //     if(inRangeItem == null)
        //         return;
        //     foreach (var uiGridObject in uiGridObjects)
        //     {
        //         uiGridObject.ExitCell();
        //     }
        //     uiGridObjects.Clear();
        //     foreach (var cell in inRangeItem.Cells)
        //     {
        //         UiGridObject uiGridObject = grid.GetGridObject(cell.transform.position);
        //         if (uiGridObject != null && uiGridObject.CanBuild())
        //         {
        //             uiGridObject.EnterCell();
        //             uiGridObjects.Add(uiGridObject);
        //         }
        //     }
        // }
        public virtual void PutDownItem(UiPackageItem item)
        {
            if(!item.CheckCanPut(grid))
                return;
            item.PutOnGrid(this);
            PackageItemPreview.Instance.ClearItem();
        }
        
        public virtual void PickUpItem()
        {
            var mousePos = GetMousePos.GetMousePosition();
            UiGridObject uiGridObject = grid.GetGridObject(mousePos);
            if (uiGridObject != null && uiGridObject.UiPackageItem != null)
            {
                var item = uiGridObject.UiPackageItem;
                item.PickOnGrid(grid);
                PackageItemPreview.Instance.SetPackageItem(item);
            }
        }
        
        public void AddItem(string itemName)
        {
            var packageItemSoData = Loader.ResourceLoad<PackageItemSoData>($"So/PackageItemData/{itemName}");
            var shapeData = packageItemSoData.shapeData;
            Vector2Int enablePoint = FindEnablePoint(shapeData);
            if (enablePoint.x == -1)
                return;
            
            PackageItemData packageItemData = new PackageItemData(itemName,enablePoint,false,1);

            UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
            uiPackageItem.InitItem(this,packageItemData);
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
