using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace game
{
    public class BaseAnimController: AbstractComponent
    {
        private readonly List<int> lockStates = new List<int>();
        private readonly List<int> forceChangeStates = new List<int>();
        
        protected virtual int curState{ get;}
        protected readonly Animator animator;
        protected readonly IAnimatorController AnimController;
        
        private bool selfStateLock;
        public bool stateLock => selfStateLock;
        
        protected Dictionary<int,int> StateDic;
        protected BaseAnimController(IAnimatorController anim,MonoBehaviour mono):base(mono)
        {
            animator = mono.GetComponentInChildren<Animator>();
            AnimController = anim;
            curState = 0;
            Init();
        }

        private void Init()
        {
            SetStateDic();
        }

        protected void AddLockState(int state)
        {
            if (!lockStates.Contains(state))
                lockStates.Add(state);
        }
        
        protected void RemoveLockState(int state)
        {
            if (lockStates.Contains(state))
                lockStates.Remove(state);
        }
        
        protected void AddForceChangeState(int state)
        {
            if (!forceChangeStates.Contains(state))
                forceChangeStates.Add(state);
        }
        
        protected void RemoveForceChangeState(int state)
        {
            if (forceChangeStates.Contains(state))
                forceChangeStates.Remove(state);
        }

        protected virtual void SetStateDic()
        {
            
        }
        
        protected virtual bool ForceChangeState(int newState)
        {
            return forceChangeStates.Contains(newState);
        }

        public void SetAnim(int state)
        {
            if(curState == state)
                return;

            //状态锁住 且无法强制改变状态
            if(stateLock && !ForceChangeState(state))
                return;
            
            
            ChangeState(state);
        }
        
        protected virtual void ChangeState(int state)
        {
            ChangeAnimState(state);
            CheckState();
            NeedLockAnim();
            WaitStateComplete();
        }
        
        protected virtual void ChangeAnimState(int state)
        {

            
        }

        private void NeedLockAnim()
        {
            foreach (var state in lockStates.Where(state => state == curState))
            {
                selfStateLock = true;
            }
        }
        
        protected virtual bool CheckSkillLockState()
        {
            return lockStates.Contains(curState) && selfStateLock;
        }

        protected virtual void CheckState()
        {
            
        }
        
        protected virtual void WaitStateComplete()
        {
            int tempState = curState;
            AnimController.AnimatorStateEnter();               
            mono.FrameEndExecute(() =>
            {
                mono.DelayExecute(animator.GetCurrentAnimatorStateInfo(0).length - StaticValue.FixedTimeScale, () => { Complete(tempState); });
            });
        }

        protected virtual void Complete(int state)
        {
            if(curState != state)
                return;
            selfStateLock = false;
            AnimController.AnimatorStateComplete();
        }
    }
}
