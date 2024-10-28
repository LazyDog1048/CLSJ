using UnityEngine;


namespace data
{
    public class MyCollider : MonoBehaviour
    {
        public ColliderData ColliderData;

        public void SetData(ColliderData data)
        {
            ColliderData = data;
            var box = gameObject.AddComponent<BoxCollider2D>();
            transform.position = data.center; 
            box.usedByComposite = true;
            box.size = data.size;
        }
    }
    
}