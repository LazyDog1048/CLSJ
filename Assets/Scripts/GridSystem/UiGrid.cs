using System;
using tool;
using UnityEngine;

namespace GridSystem
{
    public class UiGrid<T> : TopdownGrid<T>
    {
        public UiGrid(int width, int height, float cellSize, Vector3 originPos, Func<Grid<T>, int, int, T> createGridObject) : base(width, height, cellSize, originPos, createGridObject)
        {
            
        }

        protected override void DebugThing()
        {
            // if (!isDebug)
                return;
            TextMesh[,] debugTextArray = new TextMesh[width, height];
            GameObject parent = new GameObject("GridText");
            parent.transform.position = Vector3.zero;
            for (int y = 0; y < height; y++)
            {
                //先算x
                for (int x = 0; x < width; x++)
                {
                    debugTextArray[x,y] = Transform_Extend.CreateWorldText(gridArray[x,y]?.ToString(),parent.transform,GetWorldPosition(x,y) +new Vector3(cellSize,-cellSize)*0.5f,10,Color.white,TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x,y+1),Color.white,100f);
                    Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x+1,y),Color.white,100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0,height),GetWorldPosition(width,height),Color.white,100f);
            Debug.DrawLine(GetWorldPosition(width,0 ),GetWorldPosition(width,height),Color.white,100f);
            OnGridValueChanged+= (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x,eventArgs.y].text = gridArray[eventArgs.x,eventArgs.y].ToString();
            };
        }
    }
    
}
