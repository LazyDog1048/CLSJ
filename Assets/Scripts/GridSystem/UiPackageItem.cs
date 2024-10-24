using System.Collections.Generic;
using data;
using DG.Tweening;
using EquipmentSystem;
using game;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GridSystem
{
    public enum UiPackageItemState
    {
        None,
        Settle,
        Disable
    }
    public class UiPackageItem : GridGameObject,IPointerEnterHandler,IPointerExitHandler
    {
        public static bool InDetailPanel;
        public static UiPackageItem _cursorUiPackageItem;

        public static UiPackageItem cursorUiPackageItem
        {
            get=>_cursorUiPackageItem;
            private set
            {
                _cursorUiPackageItem = value;
            }
        }
        
        private PackageUiGridSystem currentGridSystem;
        public string Name => packageItemSoData.Name;
        public PackageItemSoData packageItemSoData { get; private set; }
        public PackageItemData packageItemData { get;private set; }
        [SerializeField]
        private RectTransform cellOri;
        
        private Image bg;
        private Image icon;
        private TextMeshProUGUI countText;
        
        private List<GameObject> cells;
        public List<GameObject> Cells => cells;
        private Transform cellPanel;
        public RectTransform rectTransform => transform as RectTransform;
        
        public UiPackageItemState state { get;private set; }
        
        private static Vector2 cellOffset = new Vector3(0.5f,-0.5f);
        
        private bool isRotated = true;

        private RectTransform countParent;
        private Vector3 countOriPos;
        public int Count
        {
            get => packageItemData.count;
            set
            {
                if(packageItemData == null)
                    return;
                packageItemData.count = value;
                if(countText == null)
                    return;
                countText.text = value.ToString();
                countText.transform.parent.gameObject.SetActive(value > 1);
            }
        }
        private static Color noneColor;
        private static Color hadItemColor;
        private static Color enterColor;
        private static Color cantPutColor;
        private void Awake()
        {
            bg = transform.Find("Bg").GetComponent<Image>();
            icon = transform.Find("Icon").GetComponent<Image>();
            countParent = transform.Find("Count") as RectTransform;
            countText = countParent.Find("Text").GetComponent<TextMeshProUGUI>();
            cells = new List<GameObject>();
            cellPanel = transform.Find("CellPanel");
            packageItemSoData = gridObjectSo as PackageItemSoData;
            state = UiPackageItemState.None;
        }

        public void SetLastIndex()
        {
            transform.SetAsLastSibling();
        }
        
        public void InitItem(PackageItemData packageItemData)
        {
            this.packageItemData = packageItemData;
            packageItemSoData = ResourcesDataManager.GetPackageItemSoData(packageItemData.Name);
            gameObject.name = packageItemData.Name;
            BaseInitItem(packageItemData);
            state = UiPackageItemState.None;
            bg.gameObject.SetActive(true);
            Count = packageItemData.count;
        }
        
        public void InitItem(PackageUiGridSystem gridSystem,PackageItemData packageItemData)
        {
            currentGridSystem = gridSystem;
            this.packageItemData = packageItemData;
            gameObject.name = packageItemData.Name;
            packageItemSoData = ResourcesDataManager.GetPackageItemSoData(packageItemData.Name);
            transform.SetParent(gridSystem.itemParent.transform);
            BaseInitItem(packageItemData);
            
            //计算相对位置移动
            UiGridObject first = gridSystem.Grid.GetGridObject(packageItemData.firstGridPoint);
            Vector3 offset = first.gridCellTransform.position - cells[0].transform.position;
            transform.position += offset;
            //根据cell的位置设置gridObject
            foreach (var cell in cells)
            {
                UiGridObject uiGridObject = gridSystem.Grid.GetGridObject(cell.transform.position);
                uiGridObject.SetGridItem(this); 
            }
            bg.gameObject.SetActive(true);
            Count = packageItemData.count;
            FindRightDownCell();
        }
        
        public void InitItem(PlayerEquipmentSlot slot,PackageItemSoData soData)
        {
            transform.SetParent(slot.transform);
            packageItemData = slot.packageItemData;
            gameObject.name = packageItemData.Name;
            packageItemSoData = ResourcesDataManager.GetPackageItemSoData(soData.Name);
            BaseInitItem(soData);
            FindRightDownCell();
            Count = packageItemData.count;
        }

        private void BaseInitItem(PackageItemData packageItemData)
        {
            state = UiPackageItemState.Settle;
            

            isRotated = packageItemData.isRotated;
            rectTransform.rotation = Quaternion.Euler(0, 0, isRotated ? 90 : 0);
            rectTransform.localScale = Vector3.one;
            Vector2 sizeDelta =  new Vector2(packageItemSoData.size.x,packageItemSoData.size.y) * StaticValue.pixelsPerUnit;
            rectTransform.sizeDelta = sizeDelta;
            icon.rectTransform.sizeDelta = sizeDelta;
            icon.sprite = packageItemSoData.icon;
            icon.SetNativeSize();
            countOriPos = countParent.localPosition;
            //设置cell的位置
            foreach (var shapePoint in packageItemSoData.shapeData.points)
            {
                GameObject go = Instantiate(cellOri.gameObject, cellPanel);
                RectTransform rt = go.GetComponent<RectTransform>();
                go.SetActive(true);
                cells.Add(go);
                cellOri.sizeDelta = Vector2.one * StaticValue.pixelsPerUnit;
                rt.anchoredPosition = new Vector2(shapePoint.x,-shapePoint.y) * StaticValue.pixelsPerUnit + (cellOffset * StaticValue.pixelsPerUnit);
            }
            
        }
        
        private void BaseInitItem(PackageItemSoData soData)
        {
            state = UiPackageItemState.None;

            isRotated = false;
            rectTransform.rotation = Quaternion.Euler(0, 0,0);
            rectTransform.localScale = Vector3.one;
            Vector2 sizeDelta =  new Vector2(packageItemSoData.size.x,packageItemSoData.size.y) * StaticValue.pixelsPerUnit;
            rectTransform.sizeDelta = sizeDelta;
            icon.rectTransform.sizeDelta = sizeDelta;
            icon.sprite = packageItemSoData.icon;
            icon.SetNativeSize();
            countOriPos = countParent.localPosition;
            //设置cell的位置
            foreach (var shapePoint in packageItemSoData.shapeData.points)
            {
                GameObject go = Instantiate(cellOri.gameObject, cellPanel);
                RectTransform rt = go.GetComponent<RectTransform>();
                go.SetActive(true);
                cells.Add(go);
                rt.anchoredPosition = new Vector2(shapePoint.x,-shapePoint.y) * StaticValue.pixelsPerUnit + (cellOffset * StaticValue.pixelsPerUnit);
            }
        }


        public void UpdatePosition()
        {
            var mousePos = GetMousePos.GetMousePosition();
            transform.position = mousePos;
        }

        public bool CheckAddToItem(UiGrid<UiGridObject> grid)
        {
            if(packageItemSoData.ItemType != PackageItemType.Bullet)
            {
                return false;
            }
            Debug.Log("CheckAddToItem_1");
            foreach (var cell in Cells)
            {
                UiGridObject playerGrid = grid.GetGridObject(cell.transform.position);
                if (playerGrid == null || playerGrid.CanBuild() || !playerGrid.UiPackageItem.Name.Equals(Name))
                {
                    return false;
                }
            }
            UiGridObject first = grid.GetGridObject(cells[0].transform.position);
            Debug.Log("CheckAddToItem_2");

            first.UiPackageItem.Count += packageItemData.count;
            DestroyImmediate(gameObject);
            return true;
        }
        
        public bool CheckCanPutOnGrid(UiGrid<UiGridObject> grid)
        {
            if (state != UiPackageItemState.None)
                return false;
            foreach (var cell in cells)
            {
                UiGridObject uiGridObject = grid.GetGridObject(cell.transform.position);
                if (uiGridObject == null || !uiGridObject.CanBuild())
                {
                    return false;
                }
            }
            return true;
        }
        

        public void CheckRotate()
        {
            state = UiPackageItemState.Disable;
            if (!isRotated)
            {
                PackageItemPreview.Instance.transform.DORotate(new Vector3(0, 0, 90), StaticValue.BtnAnimTime);
                PackageItemPreview.Instance.RotaBg(true);
                rectTransform.DORotate(new Vector3(0,0,90),StaticValue.BtnAnimTime).onComplete += () =>
                {
                    state = UiPackageItemState.None;
                    UpdatePosition();
                };
                isRotated = true;
            }
            else
            {
                rectTransform.DORotate(new Vector3(0, 0, 0), StaticValue.BtnAnimTime).onComplete += () =>
                {
                    state = UiPackageItemState.None;
                    UpdatePosition();
                };
                PackageItemPreview.Instance.RotaBg(false);
                isRotated = false;
            }
            FindRightDownCell();
        }

        public void FindRightDownCell()
        {
            Vector3 pos = cells[^1].transform.position;
            if (isRotated)
            {
                pos = pos.Rota2DPoint(transform.position, -90);
                countParent.localRotation = Quaternion.Euler(0,0,-90);    
                countParent.anchoredPosition = new Vector2(-18f,7.5f);
            }
            else
            {
                countParent.localRotation = Quaternion.Euler(0,0,0);
                countParent.anchoredPosition = new Vector2(-7.5f,4f);
            }
        }
        
        public void PutOnGrid(PackageUiGridSystem gridSystem)
        {
            currentGridSystem = gridSystem;
            transform.SetParent(gridSystem.itemParent);
            state = UiPackageItemState.Settle;
            // List<Vector2Int> gridPos = new List<Vector2Int>();
            foreach (var cell in cells)
            {
                UiGridObject uiGridObject = gridSystem.Grid.GetGridObject(cell.transform.position);
                uiGridObject.SetGridItem(this);
                // gridPos.Add(new Vector2Int(uiGridObject.X,uiGridObject.Y));
                // pos += uiGridObject.GetWorldPosition() + new Vector3(1,-1) / 2;
            }
            UiGridObject first = gridSystem.Grid.GetGridObject(cells[0].transform.position);
            Vector3 offset = first.gridCellTransform.position - cells[0].transform.position;
            transform.position += offset;

            bg.gameObject.SetActive(true);
            packageItemData.firstGridPoint = new Vector2Int(first.X,first.Y);
            packageItemData.isRotated = isRotated;
        }
        
        public void PutOnSlot(PlayerEquipmentSlot slot)
        {
            transform.SetParent(slot.transform);
            state = UiPackageItemState.Settle;
            transform.localScale = Vector3.one;
            rectTransform.anchoredPosition = Vector2.zero;
        }

        public void PickOnGrid(UiGrid<UiGridObject> grid)
        {
            state = UiPackageItemState.None;
            foreach (var cell in cells)
            {
                UiGridObject uiGridObject = grid.GetGridObject(cell.transform.position);
                uiGridObject.RemoveGridItem();
            }
            bg.gameObject.SetActive(false);
            Package_Panel.Instance.itemDetailPanel.ExitItemPanel();
        }
        
        public void PickOnSlot(PlayerEquipmentSlot slot)
        {
            state = UiPackageItemState.None;
            bg.gameObject.SetActive(false);
            Package_Panel.Instance.itemDetailPanel.ExitItemPanel();
        }

        // private Vector2Int pickPos;
        public void SaveItemToPackage()
        {
            LocalPackageThing.GetData().AddPackageData(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            InDetailPanel = true;
            if(currentGridSystem is not PlayerPackageUiGridSystem || PackageItemPreview.Instance.currentUiPackageItem != null)
                return;
            cursorUiPackageItem = this;
            Package_Panel.Instance.itemDetailPanel.EnterItemPanel(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InDetailPanel = false;
            cursorUiPackageItem = null;
            Package_Panel.Instance.itemDetailPanel.ExitItemPanel();
        }
    }
    
}
