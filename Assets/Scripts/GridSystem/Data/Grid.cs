using System;
using tool;
using UnityEngine;

namespace GridSystem
{
    public class Grid<TGridObject>
    {
        public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
            
        public class OnGridValueChangedEventArgs : EventArgs
        {
            public int x;
            public int y;
        }

        protected int width;
        protected int height;
        
        public int Width => width;
        public int Height => height;
        public float CellSize => cellSize;
        
        protected float cellSize;
        protected Vector3 originPos;
        protected TGridObject[,] gridArray;
        public static bool isDebug = true;
        public TGridObject[,] GridArray => gridArray;
        public Grid(int width, int height,float cellSize,Vector3 originPos,Func<Grid<TGridObject>,int,int,TGridObject> createGridObject)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPos = originPos;
            gridArray = new TGridObject[width, height];
            

            for (int y = 0; y < height; y++)
            {
                //先算x
                for (int x = 0; x < width; x++)
                {
                    gridArray[x, y] = createGridObject(this, x, y);
                }
            }

            DebugThing();

        }

        protected virtual void DebugThing()
        {
            if (!isDebug)
                return;
            TextMesh[,] debugTextArray = new TextMesh[width, height];
            GameObject parent = new GameObject("GridText");
            parent.transform.position = Vector3.zero;
            for (int y = 0; y < height; y++)
            {
                //先算x
                for (int x = 0; x < width; x++)
                {
                    debugTextArray[x,y] = Transform_Extend.CreateWorldText(gridArray[x,y]?.ToString(),parent.transform,GetWorldPosition(x,y) +new Vector3(cellSize,cellSize)*0.5f,10,Color.white,TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x,y+1),Color.white,100f);
                    Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x+1,y),Color.white,100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0,height),GetWorldPosition(width,height),Color.white,100f);
            Debug.DrawLine(GetWorldPosition(width,0),GetWorldPosition(width,height),Color.white,100f);
            OnGridValueChanged+= (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x,eventArgs.y].text = gridArray[eventArgs.x,eventArgs.y].ToString();
            };
        }
        
        public virtual Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize + originPos;
        }

        public void SetGridObject(int x, int y, TGridObject value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = value;
                TriggerGridObjectChanged(x, y);
            }
        }
        
        public void TriggerGridObjectChanged(int x, int y)
        {
            if (OnGridValueChanged != null)
                OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
        
        public void SetGridObject(Vector3 worldPosition, TGridObject value)
        {
            GetXY(worldPosition, out var x, out var y);
            SetGridObject(x, y, value);
        }
        
        public virtual void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            var localPosition = worldPosition - originPos;
            x = Mathf.FloorToInt(localPosition.x / cellSize);
            y = Mathf.FloorToInt(localPosition.y / cellSize);
        }
        
        
        public TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            else
            {
                return default(TGridObject);
            }
        }
        
        public TGridObject GetGridObject(Vector2Int position)
        {
            return GetGridObject(position.x, position.y);
        }
        
        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            GetXY(worldPosition, out var x, out var y);
            return GetGridObject(x, y);
        }
        
        public void ForEach(Action<TGridObject> action)
        {
            for (int y = 0; y < height; y++)
            {
                //先算x
                for (int x = 0; x < width; x++)
                {
                    action(gridArray[x, y]);
                }
            }
        }
    }
    
}
