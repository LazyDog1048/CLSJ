using System;
using UnityEngine;


namespace data
{
    [Serializable]
    public struct ColliderData
    {
        public Vector2 form;
        public Vector2 to;
        public float maxX;
        public float minX;
        
        public float maxY;
        public float minY;
        public ColliderData(Vector2 form, Vector2 to)
        {
            this.form = form;
            this.to = to;
            maxX = form.x > to.x? form.x : to.x;
            minX = form.x < to.x? form.x : to.x;
            maxY = form.y > to.y? form.y : to.y;
            minY = form.y < to.y? form.y : to.y;
        }
        public Vector2 center => (form + to) / 2;
        public Vector2 size => form.GetSize(to);
        
        public bool IsPointInCollider(Vector2 point)
        {
            return point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY;
        }
    }

}
