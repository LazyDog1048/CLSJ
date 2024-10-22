using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public enum GunState
    {
        Idle,
        Shot
    }

    public class GunAnimatorController : SM_Animator
    {
        public GunState CurState { get; set; }
        
        private Dictionary<GunState,int> animStateHashDic;
        
        public GunAnimatorController(MonoBehaviour mono) : base(mono)
        {
        }

        protected override void SetStateDic()
        {
            animStateHashDic = new Dictionary<GunState, int>
            {
                {GunState.Idle, AnimaHash.state_Idle},
                {GunState.Shot, AnimaHash.state_Shot},
                
            };
            
            CurState = GunState.Idle;
            // AddLockState((int)GunState.Shot);
        }

        public void ReloadAnimator(RuntimeAnimatorController animatorController)
        {
            animator.runtimeAnimatorController = animatorController;
            ResetLockState();
            SetAnim(0);
        }
        
        public void SetAnim(GunState state)
        {
            base.SetAnim((int)state);
        }

        protected override void ChangeAnimState(int state)
        {
            CurState = (GunState) state;
            animator.Play(animStateHashDic[CurState],0,0);
        }

    }
    
}
