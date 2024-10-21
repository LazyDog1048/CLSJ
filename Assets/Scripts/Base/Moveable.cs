using buff;
using UnityEngine;
using UnityEngine.Events;

namespace plug
{
    public class Moveable : AbstractComponent
    {
        private bool lockMove;
        public virtual bool CanMove => !lockMove;
        
        public virtual float MoveSpeed=> moveSpeed;
        public float moveSpeed;


        public Moveable(float Speed,MonoBehaviour mono) : base(mono)
        {
            moveSpeed = Speed;
        }



        public void ForceMove(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                target, MoveSpeed* Time.deltaTime);
        }

        public void Move(Vector2 target)
        {
            if(!CanMove)
                return;
            DoMove(target);
        }

        public void AutoMove(Vector2 target,UnityAction Complete)
        {
            MoveEnable(false);
            mono.WaitFixedExecute(()=>!transform.position.DisLongerThan(target,0.1f), () =>
            {
                Complete?.Invoke();
                MoveEnable(true);
            }, () =>
            {
                AutoDoMove(target);
            });
        }
        protected virtual void DoMove(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                target, MoveSpeed* Time.deltaTime);
        }

        private void AutoDoMove(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                target, MoveSpeed* Time.deltaTime);
        }

        protected void MoveEnable(bool enable)
        {
            lockMove = !enable;
        }
    }
}
