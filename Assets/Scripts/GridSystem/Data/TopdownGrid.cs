using System;
using tool;
using UnityEngine;

namespace GridSystem
{
    public class TopdownGrid<T> : Grid<T>
    {
        public TopdownGrid(int width, int height, float cellSize, Vector3 originPos, Func<Grid<T>, int, int, T> createGridObject) : base(width, height, cellSize, originPos, createGridObject)
        {
            
        }

        public override Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, -y) * cellSize + originPos;
        }


        public override void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            var localPosition = worldPosition - originPos;
            x = Mathf.FloorToInt(localPosition.x / cellSize);
            y = -Mathf.FloorToInt(localPosition.y / cellSize)-1;
        }
    }
    
}
