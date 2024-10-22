using UnityEngine;

namespace plug
{
    public class FaceDir : AbstractComponent
    {
        public bool isFaceToRight { get;protected set; }
        protected Vector3 leftScale;
        protected Vector3 rightScale;
        public Vector3 watchPos { get;protected set; }
        
        protected Transform bodyTransform;
        
        public FaceDir(MonoBehaviour mono) : base(mono)
        {
            bodyTransform = transform.Find("Body");
            rightScale = Vector3.one;
            leftScale = new Vector3(-rightScale.x,rightScale.y,rightScale.z);
            isFaceToRight = bodyTransform.localScale.x > 0;
        }

        public virtual void FaceToTarget(Vector2 target)
        {
            if (target.x < bodyTransform.position.x)
            {
                isFaceToRight = false;
                bodyTransform.localScale = leftScale;
            }
            else if(target.x > bodyTransform.position.x)
            {
                isFaceToRight = true;
                bodyTransform.localScale = rightScale;
            }
            watchPos = target;
        }

        public virtual void FaceToTarget(bool moveToRight)
        {
            if (moveToRight)
            {
                isFaceToRight = true;
                bodyTransform.localScale = rightScale;
            }
            else
            {
                isFaceToRight = false;
                bodyTransform.localScale = leftScale;
            }
        }
        
        public static void FaceToTarget(Transform transform,Vector2 target)
        {
            if (target.x < transform.position.x)
            {
                transform.localScale = Vector3.one;
            }
            else if(target.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
        }
    }
    
}
