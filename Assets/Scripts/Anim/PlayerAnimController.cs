using System.Collections.Generic;
using game;

namespace Player
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Run,
        AirBorne,
        Throw,
        Disappear,
        Appear,
        Charging,
        ChargingRecover,
        Dizzy,
        Skill,
        Dig,
        Pull,
        Throw_Stone,
        Roll,
        Landing,
        CoinJump,
        Surprise,
        Excavate,
        Dance_1,
        Dance_2,
        Dance_3,
        Dance_4,
        Dance_5,
        Bala_1,
        Bala_2,
        Bala_3
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
                {PlayerState.Throw, AnimaHash.state_Throw},
                {PlayerState.Disappear, AnimaHash.state_Disappear},
                {PlayerState.Appear, AnimaHash.state_Appear},
                {PlayerState.AirBorne, AnimaHash.state_Walk},
                {PlayerState.Charging, AnimaHash.state_Charging},
                {PlayerState.ChargingRecover, AnimaHash.state_ChargingRecover},
                {PlayerState.Dizzy, AnimaHash.state_Dizzy},
                {PlayerState.Skill, AnimaHash.state_Skill},
                {PlayerState.Dig, AnimaHash.state_Dig},
                {PlayerState.Pull, AnimaHash.state_Pull},
                {PlayerState.Throw_Stone, AnimaHash.state_Throw_Stone},
                {PlayerState.Roll, AnimaHash.state_Roll},
                {PlayerState.Landing, AnimaHash.state_Landing},
                {PlayerState.Dance_1, AnimaHash.state_Dance_1},
                {PlayerState.Dance_2, AnimaHash.state_Dance_2},
                {PlayerState.Dance_3, AnimaHash.state_Dance_3},
                {PlayerState.Dance_4, AnimaHash.state_Dance_4},
                {PlayerState.Dance_5, AnimaHash.state_Dance_5},
                {PlayerState.CoinJump, AnimaHash.state_CoinJump},
                {PlayerState.Surprise, AnimaHash.state_Surprise},
                {PlayerState.Excavate, AnimaHash.state_Excavate},
                {PlayerState.Bala_1, AnimaHash.state_Bala_1},
                {PlayerState.Bala_2, AnimaHash.state_Bala_2},
                {PlayerState.Bala_3, AnimaHash.state_Bala_3}
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
                case PlayerState.ChargingRecover:
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
