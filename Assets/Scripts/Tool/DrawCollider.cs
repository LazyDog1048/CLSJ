using System.Collections.Generic;
using data;
using UnityEngine;

public enum DrawMode { None, Draw, Delet }
// [RequireComponent(typeof(CompositeCollider2D))]
public class DrawCollider : MonoBehaviour
{
    public DrawMode DrawMode; 
    public bool isDrawChildCollider;

    public List<MyCollider> myColliders
    {
        get
        {
            var list = new List<MyCollider>();
            for(var i=0;i<transform.childCount;i++)
            {
                var child = transform.GetChild(i);
                if (child.GetComponent<MyCollider>() == null) continue;
                MyCollider myCollider = child.GetComponent<MyCollider>();
                list.Add(myCollider);
            }
            return list;
        }
    }

    public void AddDrawCollider(ColliderData colliderData)
    {
        GameObject obj = new GameObject($"Collider_{transform.childCount}");
        obj.transform.SetParent(transform);
        MyCollider collider = obj.AddComponent<MyCollider>();
        collider.SetData(colliderData);
    }
}
