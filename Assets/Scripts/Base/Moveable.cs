using buff;
using UnityEngine;
using UnityEngine.Events;

namespace plug
{
    public class Moveable : AbstractComponent
    {
        private bool lockMove;

        public bool AbStateLock => lockMove;
        public virtual bool CanMove => !lockMove;
        public EventExtraFloat moveSpeed;
        private float speed;
        

        public Moveable(float Speed,MonoBehaviour mono) : base(mono)
        {
            speed = Speed;
            moveSpeed = new EventExtraFloat(speed);
            moveSpeed.valueChange.AddListener(SpeedChange);
        }

        protected virtual void SpeedChange()
        {
            
        }


        public void ForceMove(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                target, moveSpeed.FinalValue* Time.deltaTime);
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
                target, moveSpeed.FinalValue* Time.deltaTime);
        }

        private void AutoDoMove(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                target, moveSpeed.FinalValue* Time.deltaTime);
        }

        protected void MoveEnable(bool enable)
        {
            lockMove = !enable;
        }
    }
}
