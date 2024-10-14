using System;
using System.Collections;
using System.Collections.Generic;
using tool;
using UnityEngine;

namespace GridSystem
{
    public class TopdownGrid<T> : Grid<T>
    {
        public TopdownGrid(int width, int height, float cellSize, Vector3 originPos, Func<Grid<T>, int, int, T> createGridObject) : base(width, height, cellSize, originPos, createGridObject)
        {
            
        }
        
        protected override void DebugThing()
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
                    debugTextArray[x,y] = Transform_Extend.CreateWorldText(gridArray[x,y]?.ToString(),parent.transform,GetWorldPosition(x,y) +new Vector3(cellSize,-cellSize)*0.5f,10,Color.white,TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x,y-1),Color.white,100f);
                    Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x+1,y),Color.white,100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0,-1),GetWorldPosition(width,-1),Color.white,100f);
            Debug.DrawLine(GetWorldPosition(width,-1 ),GetWorldPosition(width,height-1),Color.white,100f);
            OnGridValueChanged+= (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x,eventArgs.y].text = gridArray[eventArgs.x,eventArgs.y].ToString();
            };
        }
        
        public override Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, -y) * cellSize + originPos;
        }


        public override void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            var localPosition = worldPosition - originPos;
            x = Mathf.FloorToInt(localPosition.x / cellSize);
            y = -Mathf.FloorToInt(localPosition.y / cellSize);
        }
    }
    
}
