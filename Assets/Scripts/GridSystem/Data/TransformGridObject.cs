using UnityEngine;

namespace GridSystem
{
    public class TransformGridObject<T> : GridObject<T>
    {

        protected TransformGridObject(Grid<T> grid, int x, int y) : base(grid, x, y)
        {
        }
        
        public virtual void SetGridAnchorItem(Transform item)
        {
            TriggerGridObjectChanged();
        }
        
        public virtual void RemoveGridItem()
        {
            TriggerGridObjectChanged();
        }

        public override string ToString()
        {
            return "";
        }
    }
}
