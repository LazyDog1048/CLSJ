using game;
using UnityEngine;
using UnityEngine.UI;

namespace GridSystem
{
    public class UiGridObject : TransformGridObject<UiGridObject>
    {
        //底部的单个cell
        
        public RectTransform gridCellTransform { get;private set; }
        private Image image;
        public static Vector3 cellOffset = new Vector3(0.5f,0.5f,0);
        //放置在cell上的物体
        public  UiPackageItem UiPackageItem => uiPackageItem;
        private UiPackageItem uiPackageItem;
        public bool HasItem => uiPackageItem != null;
        protected float uiCellSize = 11;

        public UiGridObject(Grid<UiGridObject> grid, int x, int y) : base(grid, x, y)
        {
            
            // uiCellSize = (grid as UiGrid<UiGridObject>).uiCellSize;
        }
        
        public void InitGameObject(Transform gridTrans)
        {
            image = gridTrans.GetComponent<Image>();
            gridCellTransform = image.rectTransform;
            gridCellTransform.gameObject.SetActive(true);
            gridCellTransform.sizeDelta = Vector2.one * uiCellSize;
            gridCellTransform.anchoredPosition = new Vector2(X,-Y)*uiCellSize + new Vector2(uiCellSize,-uiCellSize)/2;
            // Vector3 gridPos = GetWorldPosition();
            // gridCellTransform.position = gridPos + cellOffset;
            // gridCellTransform.position = gridPos;
            gridCellTransform.localRotation = Quaternion.identity;
            gridCellTransform.localScale = Vector3.one;
            // (gridCellTransform as RectTransform).sizeDelta = Vector2.one * StaticValue.pixelsPerUnit; 
            gridTrans.name = $"{X},{Y}";
            // image.enabled = false;
        }
        
   
        public void SetGridItem(UiPackageItem item)
        {
            // image.color = hadItemColor;
            uiPackageItem = item;
            base.SetGridAnchorItem(item.transform);
            SetGridObjectSo(item.packageItemSoData);
        }


        public override void RemoveGridItem()
        {
            // image.color = noneColor;
            uiPackageItem = null;
            base.RemoveGridItem();
            RemoveGridObjectSo();
        }
        
        public bool CanBuild()
        {
            return uiPackageItem == null;
        }
        
    }
}
