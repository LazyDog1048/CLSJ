using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class DrawUtils
    {
        private static DateTime lastIsometricErrorShown = DateTime.MinValue;

        /// <summary>
        /// Draws an outline around the rectangle formed by the two given points.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fromTile">First point of the rectangle.</param>
        /// <param name="toTile">Second point of the rectangle.</param>
        /// <param name="color">Color of the outline</param>
        /// <param name="sizeModifier">How much smaller should the outline be than the actual grid tiles</param>
        /// <param name="addDiagonal">Whether a diagonal should be drawn</param>
        /// <param name="label">Optional label</param>
        public static void DrawRectangleOutline(Vector2 fromTile, Vector2 toTile, Color color, bool addDiagonal = false, string label = null)
        {
            var xDirection = new Vector2(toTile.x - fromTile.x, 0);
            var yDirection = new Vector2(0, toTile.y - fromTile.y);

            var second = fromTile + xDirection;
            var fourth = fromTile + yDirection;
            
            Vector2[] points = {fromTile, second, toTile, fourth};

            
            var originalColor = Handles.color;
            Handles.color = color;

            if (!string.IsNullOrEmpty(label))
            {
                var size = HandleUtility.GetHandleSize(points[1] + new Vector2(0.02f, 0));

                var style = new GUIStyle();
                style.normal.textColor = color;
                style.fontSize = (int) (15 / size);

                Handles.Label(points[1] + new Vector2(0.08f, 0), label, style);
            }
            // Debug.Log($"draw {points[0]} {points[1]} {points[2]} {points[3]}");
            Handles.DrawLine(points[0], points[1]);
            Handles.DrawLine(points[1], points[2]);
            Handles.DrawLine(points[2], points[3]);
            Handles.DrawLine(points[3], points[0]);

            if (addDiagonal)
            {
                Handles.DrawLine(points[0], points[2]);
            }

            Handles.color = originalColor;
        }
    }    
}

