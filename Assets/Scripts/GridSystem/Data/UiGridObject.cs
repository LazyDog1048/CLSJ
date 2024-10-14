using game;
using item;
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
        public UiGridObject(Grid<UiGridObject> grid, int x, int y) : base(grid, x, y)
        {
        }
        
        public void InitGameObject(Transform gridTrans)
        {
            image = gridTrans.GetComponent<Image>();
            gridCellTransform = image.rectTransform;
            Vector3 gridPos = GetWorldPosition();
            gridCellTransform.gameObject.SetActive(true);
            gridCellTransform.position = gridPos + cellOffset;
            gridCellTransform.localRotation = Quaternion.identity;
            gridCellTransform.localScale = Vector3.one;
            (gridCellTransform as RectTransform).sizeDelta = Vector2.one * StaticValue.pixelsPerUnit; 
            gridTrans.name = $"{X},{Y}";
        }
        
        public void EnterCell()
        {
            image.color = Color.green;
        }
        
        public void ExitCell()
        {
            image.color = Color.white;
        }
        public void SetGridItem(UiPackageItem item)
        {
            image.color = Color.blue;
            uiPackageItem = item;
            base.SetGridAnchorItem(item.transform);
            SetGridObjectSo(item.packageItemSoData);
        }


        public override void RemoveGridItem()
        {
            image.color = Color.white;
            uiPackageItem = null;
            base.RemoveGridItem();
            RemoveGridObjectSo();
        }
        
    }
}
