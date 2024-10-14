using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace item
{
    public class GridAnchorItem : MonoBehaviour
    {
        
        public Vector2Int ItemSize => buildingSo.size;
        private List<BuildingObject>  buildingObjectList;
        private BuildingObject  anchorBuildingObject => buildingObjectList[0];
        private BuildingObjectSo  buildingSo;

        private Vector3 anchorPosition => anchorBuildingObject.GetWorldPosition();
        private Vector3 centerPosition => anchorPosition - new Vector3(ItemSize.x / 2f, ItemSize.y / 2f);

        private List<Vector2Int> gridPositionList = new List<Vector2Int>();
        public static GridAnchorItem Create(Grid<BuildingObject> grid,Vector3 buildPosition, BuildingObjectSo so)
        {
            
            Transform placedObjectTransform = Instantiate(so.prefab.transform, buildPosition, Quaternion.identity);
            GridAnchorItem placedObject = placedObjectTransform.GetComponent<GridAnchorItem>();
            placedObject.SetUp(grid,buildPosition,so);
            return placedObject;
        }

        private void SetUp(Grid<BuildingObject> grid,Vector3 choosePos,BuildingObjectSo so)
        {
            buildingSo = so;
            Vector3 anchorPos = choosePos - new Vector3Int(so.size.x/2,so.size.y/2);
            buildingObjectList = GetGridPositionList(grid,anchorPos);
            transform.position = anchorPosition;
        }
        
        public void UpdateAnchorPosition()
        {
            transform.position = centerPosition;
        }
        
        public  List<BuildingObject> GetGridPositionList(Grid<BuildingObject> grid,Vector3 anchorPos) 
        {
            BuildingObject anchorBuildingObject = grid.GetGridObject(anchorPos);
            List<BuildingObject> gridPositionList = new List<BuildingObject>();
            BuildingObject gridObject = null;
            for (int x = 0; x < ItemSize.x; x++) {
                for (int y = 0; y < ItemSize.y; y++) {
                    gridObject = grid.GetGridObject(anchorBuildingObject.X+ x, anchorBuildingObject.Y + y);
                    // Debug.Log($"{gridObject.X} {gridObject.Y} {gridObject.CanBuild()}");
                    gridObject.SetGridAnchorItem(this);
                    gridPositionList.Add(gridObject);
                }
            }
            return gridPositionList;
        }
    }
    
}
