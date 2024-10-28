using data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MyCollider))]
    public class MyCollider_Inspector : UnityEditor.Editor
    {
        public void OnSceneGUI()
        {
            var collider = target as MyCollider;
            DrawUtils.DrawRectangleOutline(collider.ColliderData.form, collider.ColliderData.to,Color.red,true);
        }
    }
    
}
