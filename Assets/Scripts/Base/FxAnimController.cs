using other;
using UnityEngine;

namespace game
{
    public class FxAnimController : SM_Animator
    {
        public FxAnimController(MonoBehaviour mono) : base(mono)
        {
        }

        public void SetAnimNull()
        {
            animator.runtimeAnimatorController = null;
        }
        public void ReloadAnimator(RuntimeAnimatorController animatorController)
        {
            animator.runtimeAnimatorController = animatorController;
            ResetLockState();
            SetAnim(0);
        }

        protected override void SetAnim(int state)
        {
            ChangeAnimState(state);
            curState = state;
            WaitStateComplete();
        }
        protected override void ChangeAnimState(int state)
        {
            animator.Play(AnimaHash.state_Play,0,0);
        }
    }
}
