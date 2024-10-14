// using System.Collections.Generic;
// using EquipmentSystem;
// using GridSystem;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace item
// {
//     public class UiGridAnchorItem : BaseGridAnchorItem<UiGridObject>
//     {
//         private Image image;
//         RectTransform rectTransform;
//         private Vector2Int packagePos;
//         public static UiGridAnchorItem Create(Grid<UiGridObject> grid,Vector2 anchorPosition,
//             GridObjectSo so,GameObject uiAnchorItemOri,List<UiGridObject> uiGridObjects)
//         {
//             Transform placedObjectTransform = Instantiate(uiAnchorItemOri.transform,Package_Panel.Instance.transform);
//             UiGridAnchorItem placedObject = placedObjectTransform.GetComponent<UiGridAnchorItem>();
//             placedObject.SetUp(grid,anchorPosition,so,uiGridObjects);
//             return placedObject;
//         }
//
//         private void SetUp(Grid<UiGridObject> grid,Vector2 anchorPosition,GridObjectSo so,List<UiGridObject> uiGridObjects)
//         {
//             gridObjectSo = so;
//             
//             rectTransform = GetComponent<RectTransform>();
//             image = GetComponent<Image>();
//             image.sprite = gridObjectSo.prefab.GetComponentInChildren<SpriteRenderer>().sprite;
//             rectTransform.sizeDelta = new Vector2(gridObjectSo.size.x,gridObjectSo.size.y) * grid.CellSize * Package_Panel.CellSize;
//             rectTransform.localScale = Vector3.one;
//             
//             grid.GetXY(anchorPosition, out int x, out int y);
//             packagePos = new Vector2Int(x,y);
//             LocalPackageThing.GetData().AddPackageData(packagePos,so.Name);
//             
//             UpdateItem(anchorPosition,uiGridObjects);
//             
//         }
//
//         public void UpdateItem(Vector2 anchorPosition,List<UiGridObject> uiGridObjects)
//         {
//             this.anchorPosition = anchorPosition;
//             gridObjectList = uiGridObjects;
//             transform.position = anchorPosition;
//             transform.rotation = Quaternion.identity;
//             PutDownItem();
//             Package_Panel.Instance.Grid.GetXY(anchorPosition, out int x, out int y);
//             var newPackagePos = new Vector2Int(x,y);
//             LocalPackageThing.GetData().UpdateEquipmentPosition(packagePos,newPackagePos);
//             packagePos = newPackagePos;
//         }
//         public void PickUpItem()
//         {
//             gameObject.SetActive(false);
//             foreach (var gridObject in gridObjectList)
//             {
//                 gridObject.RemoveTransform();
//             }
//         }
//         
//         public void PutDownItem()
//         {
//             gameObject.SetActive(true);
//             
//             foreach (var gridObject in gridObjectList)
//             {
//                 gridObject.SetGridAnchorItem(this,gridObjectSo);
//             }
//         }
//         
//         public void RemoveItem()
//         {
//             LocalPackageThing.GetData().RemovePackageData(packagePos);
//             foreach (var gridObject in gridObjectList)
//             {
//                 gridObject.RemoveTransform();
//             }
//             Destroy(gameObject);
//         }
//     }
//     
// }
