using System.Collections.Generic;
using game;

namespace Player
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Run,
        Dead
    }
    
    public  class PlayerAnimController:BaseAnimController
    {
        public PlayerState CurState { get; set; }
        public PlayerState PreviousState { get; set; }
        protected override int curState => (int)CurState;
        private Dictionary<PlayerState,int> animStateHashDic;
        
        public PlayerAnimController(PlayerController player) : base(player, player)
        {
        }
        
        protected override void SetStateDic()
        {
            animStateHashDic = new Dictionary<PlayerState, int>
            {
                {PlayerState.Idle, AnimaHash.state_Idle},
                {PlayerState.Walk, AnimaHash.state_Walk},
                {PlayerState.Run, AnimaHash.state_Run},
                {PlayerState.Dead, AnimaHash.state_Dead},
            };
            
            CurState = PlayerState.Idle;
        }

        public void SetAnim(PlayerState state)
        {
            base.SetAnim((int)state);
        }

        protected override bool ForceChangeState(int newState)
        {
            switch ((PlayerState)newState)
            {
                case PlayerState.Dead:
                    return true;
                default:
                    return false;
            }
        }

        protected override void ChangeAnimState(int state)
        {
            PreviousState = CurState;
            CurState = (PlayerState) state;
            animator.Play(animStateHashDic[CurState],0,0);
        }

    }
}
