using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
// using Sirenix.Utilities;
using UnityEngine;

namespace data
{   
    [Serializable]
    public class Shape_Data
    {
        [OnValueChanged(nameof(ChangeSize))]
        public Vector2Int size = Vector2Int.one;

        [HideInInspector]
        public List<Vector2Int> points = new List<Vector2Int>();
        [ShowInInspector,DoNotDrawAsReference]
        [OnValueChanged(nameof(UpdateTemp))]
        [TableMatrix(HorizontalTitle = "Custom Cell Drawing", DrawElementMethod = "DrawColoredEnumElement", SquareCells = true)]
        public bool[,] matrix;
        
        [HideInInspector]
        [SerializeField]
        private bool[] keeper;
        
        private static bool DrawColoredEnumElement(Rect rect, bool value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                value = !value;
                GUI.changed = true;
                Event.current.Use();
            }
            UnityEditor.EditorGUI.DrawRect(rect.Padding(1), value ? new Color(0.1f, 0.8f, 0.2f) : new Color(0, 0, 0, 0.5f));
            
            return value;
        }
        //
        [OnInspectorInit]
        private void CreateData()
        {
            matrix = new bool[size.x, size.y];
            if (keeper == null)
            {
                keeper = new bool[size.x * size.y];
            }
            else
            {
                if (keeper.Length != size.x * size.y)
                {
                    keeper = new bool[size.x * size.y];
                    return;
                }
                points.Clear();
                for (int x = 0; x < size.x; x++)
                {
                    for (int y = 0; y < size.y; y++)
                    {
                        matrix[x, y] = keeper[x * size.y + y];
                        if(matrix[x, y])
                            points.Add(new Vector2Int(x,y));
                    }
                }
            }
        }

        private void ChangeSize()
        {
            matrix = new bool[size.x, size.y];
            keeper = new bool[size.x * size.y];
        }

        private void UpdateTemp()
        {
            points.Clear();
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    int index = x * size.y + y;
                    keeper[index] = matrix[x, y];
                    if(matrix[x, y])
                        points.Add(new Vector2Int(x,y));
                    Debug.Log($"x  {x}  y  {y}  {matrix[x, y]}");
                }
            }
        }

        public bool this[int x, int y]
        {
            get
            {
                var index = x * size.y + y;
                if (index < keeper.Length) return keeper[index];
                Debug.LogError($"index {index}  {x}  {y}  {size.x}  {size.y}");
                return false;
            }
        }

        // [Button]
        // public void Rotation()
        // {
        //     Vector2 offSet = new Vector2(-x / 2f, y / 2f) + new Vector2(0.5f,-0.5f);
        //     Vector2[] list = new Vector2[x * y];
        //
        //     for (int i = 0; i < x; i++)
        //     {
        //         for (int j = 0; j < y; j++)
        //         {
        //             // Debug.Log($"x {i}  y {j}  {matrix[i, j]}  {i * x + j}");
        //             Vector2 point = offSet + new Vector2(i, j);
        //             list[i * x + j] = point;
        //             Vector2 rota = VectorThing.Rota2DPoint(point, Vector3.zero, 90);
        //             Debug.Log($"befor {i},{j}   {point}   after  {rota - offSet}   {rota}");
        //         }
        //     }
        // }
    }

}



