using System;
using System.Collections;
using System.Collections.Generic;
using data;
using DG.Tweening;
using EquipmentSystem;
using game;
using tool;
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
    public class UiPackageItem : GridGameObject
    {
        public PackageItemSoData packageItemSoData { get; private set; }
        private PackageItemData packageItemData { get; set; }
        [SerializeField]
        private RectTransform cellOri;
        
        private Image image;
        private List<GameObject> cells;

        private Transform cellPanel;
        private RectTransform rectTransform => transform as RectTransform;
        public UiPackageItemState state { get;private set; }
        public List<GameObject> Cells => cells;
        public bool CantPick => state == UiPackageItemState.None;
        private static Vector2 cellOffset = new Vector3(0.5f,-0.5f);
        
        private bool isRotated = true;
        
        private void Awake()
        {
            image = transform.Find("Icon").GetComponent<Image>();
            cells = new List<GameObject>();
            cellPanel = transform.Find("CellPanel");
            packageItemSoData = gridObjectSo as PackageItemSoData;
            state = UiPackageItemState.None;
        }

        public void UpdatePosition()
        {
            var mousePos = GetMousePos.GetMousePosition();
            transform.position = mousePos;
        }

        public bool CheckCanPut(UiGrid<UiGridObject> grid)
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
                isRotated = false;
            }
        }
        
        public void PutOnGrid(PackageUiGridSystem gridSystem)
        {
            transform.SetParent(gridSystem.itemParent);
            state = UiPackageItemState.Settle;
            Vector3 pos =Vector3.zero;
            List<Vector2Int> gridPos = new List<Vector2Int>();
            foreach (var cell in cells)
            {
                UiGridObject uiGridObject = gridSystem.Grid.GetGridObject(cell.transform.position);
                uiGridObject.SetGridItem(this);
                // cell.transform.position = uiGridObject.GetWorldPosition();
                gridPos.Add(new Vector2Int(uiGridObject.X,uiGridObject.Y));
                pos += uiGridObject.GetWorldPosition() + new Vector3(1,1) / 2;
            }
            transform.position = pos / cells.Count;
        }

        public void SaveItemToPackage(PackageUiGridSystem gridSystem)
        {
            UiGridObject obj = gridSystem.Grid.GetGridObject(cells[0].transform.position);
            packageItemData.firstGridPoint = new Vector2Int(obj.X,obj.Y);
            LocalPackageThing.GetData().AddPackageData(packageItemData.Name ,new Vector2Int(obj.X,obj.Y),isRotated);
        }
        
        public void RemoveItemToPackage(PackageUiGridSystem gridSystem)
        {
            UiGridObject obj = gridSystem.Grid.GetGridObject(cells[0].transform.position);
            Vector2Int first = new Vector2Int(obj.X,obj.Y);
            LocalPackageThing.GetData().RemovePackageData(first);
        }
        public void PickOnGrid(UiGrid<UiGridObject> grid)
        {
            state = UiPackageItemState.None;
            foreach (var cell in cells)
            {
                UiGridObject uiGridObject = grid.GetGridObject(cell.transform.position);
                uiGridObject.RemoveGridItem();
            }
        }
        
        public void InitItem(PackageUiGridSystem gridSystem,PackageItemData packageItemData)
        {
            this.packageItemData = packageItemData;
            packageItemSoData = Loader.ResourceLoad<PackageItemSoData>($"So/PackageItemData/{packageItemData.Name}");
            
            transform.SetParent(gridSystem.itemParent.transform);
            isRotated = packageItemData.isRotated;
            rectTransform.rotation = Quaternion.Euler(0, 0, isRotated ? 90 : 0);
            rectTransform.localScale = Vector3.one;
            Vector2 sizeDelta =  packageItemSoData.size * StaticValue.pixelsPerUnit;
            rectTransform.sizeDelta = sizeDelta;
            image.rectTransform.sizeDelta = sizeDelta;
            image.sprite = packageItemSoData.icon;
            //设置cell的位置
            foreach (var shapePoint in packageItemSoData.shapeData.points)
            {
                GameObject go = Instantiate(cellOri.gameObject, cellPanel);
                RectTransform rt = go.GetComponent<RectTransform>();
                go.SetActive(true);
                cells.Add(go);
                rt.anchoredPosition = new Vector2Int(shapePoint.x,-shapePoint.y) * StaticValue.pixelsPerUnit + (cellOffset * StaticValue.pixelsPerUnit);
            }
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

            state = UiPackageItemState.Settle;
        }

    }
    
}
