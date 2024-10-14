using System;
using UnityEngine;

namespace GridSystem
{
    public class UiGrid<T> : TopdownGrid<T>
    {
        public UiGrid(int width, int height, float cellSize, Vector3 originPos, Func<Grid<T>, int, int, T> createGridObject) : base(width, height, cellSize, originPos, createGridObject)
        {
        }
    }
    
}
