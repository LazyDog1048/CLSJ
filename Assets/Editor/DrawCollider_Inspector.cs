using data;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(DrawCollider))]
    public class DrawCollider_Inspector : UnityEditor.Editor
    {
        private Vector2 firstPoint;
        private Vector2 currentPoint;
        private bool hasFirstPos;

        public void OnEnable()
        {
            Debug.Log("OnEnable");
            hasFirstPos = false;
            SceneView.RepaintAll();
        }
        
        public void OnSceneGUI()
        {
            var draw = target as DrawCollider;
            currentPoint = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
            
            // Debug.Log($"{Event.current.isKey} {Event.current.keyCode} {Input.GetKeyDown(KeyCode.RightShift)}");
            
            if (Event.current.OnKeyDown(KeyCode.Alpha1))
            {
                switch (draw.DrawMode)
                {
                    case DrawMode.None:
                        draw.DrawMode = DrawMode.Draw;
                        break;
                    case DrawMode.Draw:
                        draw.DrawMode = DrawMode.Delet;
                        break;
                    case DrawMode.Delet:
                        draw.DrawMode = DrawMode.None;
                        break;
                }   
            }

            if (Event.current.OnKeyDown(KeyCode.Alpha2))
            {
                draw.isDrawChildCollider = !draw.isDrawChildCollider;
            }
            
            if(draw.isDrawChildCollider)
                DrawChild();
            
            switch (draw.DrawMode)
            {
                case DrawMode.Draw:
                    CheckDraw();
                    break;
                case DrawMode.Delet:
                    CheckDeleteChild();
                    break;
            }
        }

        #region DrawMode.Draw

        private void CheckDraw()
        {
            var draw = target as DrawCollider;
            var e = Event.current;
            Selection.activeGameObject = draw.transform.gameObject;
            var controlId = GUIUtility.GetControlID(FocusType.Passive);
            HandleUtility.AddDefaultControl(controlId);
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        // Debug.Log("MouseDown");
                        firstPoint = currentPoint;
                        hasFirstPos = true;
                        e.Use();
                    }
                    break;
                case EventType.MouseUp:
                    if (e.button == 0)
                    {
                        // Debug.Log("MouseUp");
                        CancelDraw();
                        e.Use();
                    }
                    break;

            }

            if (hasFirstPos)
            {
                DrawUtils.DrawRectangleOutline(firstPoint, currentPoint,Color.red,true);
            }
        }

        private void DrawChild()
        {
            var draw = target as DrawCollider;
            for(int i=0;i<draw.transform.childCount;i++)
            {
                var child = draw.transform.GetChild(i);
                if (child.GetComponent<MyCollider>() != null)
                {
                    MyCollider myCollider = child.GetComponent<MyCollider>();
                    DrawUtils.DrawRectangleOutline(myCollider.ColliderData.form, myCollider.ColliderData.to,Color.green,true);
                }
            }
        }
        
        private void CancelDraw()
        {
            //to short
            hasFirstPos = false;

            if(Mathf.Abs(firstPoint.x - currentPoint.x) < 0.1f || Mathf.Abs(firstPoint.y - currentPoint.y) < 0.1f)
                return;
            var draw = target as DrawCollider;
            draw.AddDrawCollider(new ColliderData(firstPoint,currentPoint));
            
        }
        #endregion

        #region DrawMode.Delet
        private void CheckDeleteChild()
        {
            var draw = target as DrawCollider;
            var e = Event.current;
            Selection.activeGameObject = draw.transform.gameObject;
            var controlId = GUIUtility.GetControlID(FocusType.Passive);
            HandleUtility.AddDefaultControl(controlId);

            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        hasFirstPos = true;
                        e.Use();
                    }
                    break;
                case EventType.MouseUp:
                    if (e.button == 0 && hasFirstPos)
                    {
                        DeleteChild();
                        hasFirstPos = false;
                        e.Use();
                    }
                    break;
            }

            DrawDeleteChild();
           
        }

        private void DrawDeleteChild()
        {
            var draw = target as DrawCollider;
            foreach (var collider in draw.myColliders)
            {
                ColliderData colliderData = collider.ColliderData;
                if (colliderData.IsPointInCollider(currentPoint))
                {
                    DrawUtils.DrawRectangleOutline(colliderData.form, colliderData.to,hasFirstPos? Color.red :Color.yellow,true);
                }
            }
        }
    
        private void DeleteChild()
        {
            var draw = target as DrawCollider;
            for(int i= draw.myColliders.Count-1; i>=0; i--)
            {
                var child = draw.myColliders[i];
                ColliderData colliderData = child.ColliderData;
                if (colliderData.IsPointInCollider(currentPoint))
                {
                    DestroyImmediate(child.gameObject);
                }
            }
        }
        #endregion
      
        
        
        
        private enum Mode
        {
            Idle, AddDoors, DeleteDoors
        }
    }
    
}