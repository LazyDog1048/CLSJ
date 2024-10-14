using item;
using UnityEngine;

namespace GridSystem
{
    public class BuildingObject : GridObject<BuildingObject>
    {
        private GridAnchorItem anchorItem;
        
        public GridAnchorItem AnchorItem => anchorItem;

        private BuildingObjectSo BuildingTypeSo;
        public BuildingObject(Grid<BuildingObject> grid, int x, int y) : base(grid, x, y)
        {
        }
        
        public void SetGridAnchorItem(GridAnchorItem item)
        {
            this.anchorItem = item;
            TriggerGridObjectChanged();
        }
        
        public void RemoveTransform()
        {
            anchorItem = null;
            TriggerGridObjectChanged();
        }
        
        public bool CanBuild()
        {
            return anchorItem == null;
        }
        
        public override float GetNormalizedValue()
        {
            return CanBuild() ? 0 : 1;
        }
        
        public override string ToString()
        {
            return "";
        }
        
        public static bool CheckCanBuild(Grid<BuildingObject> grid,Vector2Int size,Vector3 mousePos) 
        {
            Vector3 anchorPos = mousePos - new Vector3Int(size.x/2,size.y/2);
            BuildingObject anchorBuildingObject = grid.GetGridObject(anchorPos);
            if(anchorBuildingObject == null || !anchorBuildingObject.CanBuild()) 
                return false;

            for (int x = 0; x < size.x; x++) {
                for (int y = 0; y < size.y; y++) {
                    var gridObject = grid.GetGridObject(anchorBuildingObject.X+ x, anchorBuildingObject.Y + y);
                    // Debug.Log($"{gridObject.X} {gridObject.Y} {gridObject.CanBuild()}");
                    if (gridObject == null || !gridObject.CanBuild()) 
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }
    }
}
