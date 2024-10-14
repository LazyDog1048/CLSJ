using UnityEngine;

namespace GridSystem
{
    public class TransformGridObject<T> : GridObject<T>
    {
        private Transform anchorItem;
        
        public Transform AnchorItem => anchorItem;

        protected TransformGridObject(Grid<T> grid, int x, int y) : base(grid, x, y)
        {
        }
        
        public virtual void SetGridAnchorItem(Transform item)
        {
            anchorItem = item;
            TriggerGridObjectChanged();
        }
        
        public virtual void RemoveGridItem()
        {
            anchorItem = null;
            TriggerGridObjectChanged();
        }
        
        public virtual bool CanBuild()
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
        
        public static bool CheckCanBuild<TItem>(Grid<TItem> grid,Vector2Int size,Vector3 mousePos) where TItem : TransformGridObject<T>
        {
            Vector3 anchorPos = mousePos - new Vector3Int(size.x/2,size.y/2);
            TItem anchorBuildingObject = grid.GetGridObject(anchorPos);
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
