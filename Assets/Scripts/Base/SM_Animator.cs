using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace game
{
    public class SM_Animator
    {
        private List<int> lockState = new List<int>();
        protected bool stateLock => selfStateLock;
        protected int curState{ get; set; }

        protected Animator  animator;
        protected IAnimatorController AnimatorController;
        protected bool selfStateLock;
        protected MonoBehaviour mono;

        public SM_Animator(MonoBehaviour mono)
        {
            this.mono = mono;
            animator = mono.GetComponentInChildren<Animator>();
            AnimatorController = mono.GetComponentInChildren<IAnimatorController>();
            curState = 0;
            Init();
        }

        private void Init()
        {
            SetStateDic();
        }
        
        protected virtual void SetStateDic()
        {
            
        }

        protected void AddLockState(int state)
        {
            if (!lockState.Contains(state))
                lockState.Add(state);
        }

        public void ResetState()
        {
            selfStateLock = false;
        }
        protected virtual void ResetLockState()
        {
            selfStateLock = false;
        }
        protected virtual bool ForceChangeState(int newState)
        {
            return false;
        }


        protected virtual void SetAnim(int state)
        {
            if(curState == state)
                return;
            //状态锁住 且无法强制改变状态
            if(stateLock && !ForceChangeState(state))
                return;
            ChangeAnimState(state);
            curState = state;
            NeedLockAnim();
            WaitStateComplete();
        }

        
        protected virtual void ChangeAnimState(int state)
        {

            
        }

        protected void NeedLockAnim()
        {
            foreach (var state in lockState.Where(state => state == curState))
            {
                selfStateLock = true;
            }
        }

        
        protected virtual void WaitStateComplete()
        {
            int tempState = curState;
            AnimatorController.AnimatorStateEnter();
            mono.FrameEndExecute(() =>
            {
                mono.DelayExecute(animator.GetCurrentAnimatorStateInfo(0).length - StaticValue.FixedTimeScale, () => { Complete(tempState); });
            });
        }

        protected virtual void Complete(int state)
        {
            if(curState!=state)
                return;
            selfStateLock = false;
            AnimatorController.AnimatorStateComplete();
        }
    }
}
