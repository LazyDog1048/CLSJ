using UnityEngine;

namespace GridSystem
{
    public class GridObject<T>
    {
        private Grid<T> grid;
        private int x;
        private int y;

        public GridObjectSo GridObjectSo => gridObjectSo;
        private GridObjectSo gridObjectSo;
        public GridState State => state;
        private GridState state;
        
        
        public int X => x;
        public int Y => y;
        public float CellSize => grid.CellSize;
        protected GridObject(Grid<T> grid,int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        protected void TriggerGridObjectChanged()
        {
            grid.TriggerGridObjectChanged(x, y);
        }

        public override string ToString()
        {
            return x+ "," + y;
            // Vector2Int vector2Int = new Vector2Int((int)GetWorldPosition().x, (int)GetWorldPosition().y);
            // return vector2Int.ToString();
        }
        
        public T GetGridObject()
        {
            return grid.GetGridObject(x, y);
        }
        
        public Vector3 GetWorldPosition()
        {
            return grid.GetWorldPosition(x, y);
        }
        
        public virtual Vector3 GetCenterWorldPosition()
        {
            return grid.GetWorldPosition(x, y) + new Vector3(grid.CellSize,grid.CellSize) / 2;
        }


        public virtual float GetNormalizedValue()
        {
            return 0;
        }
        
        public void SetGridObjectSo(GridObjectSo so)
        {
            gridObjectSo = so;
            state = GridState.Used;
        }
        
        public void RemoveGridObjectSo()
        {
            gridObjectSo = null;
            state = GridState.Nothing;
        }
    }
    
    public enum GridState
    {
        Nothing,
        Used,
    }
}
