using System.Collections.Generic;
using game;
using UnityEngine;

namespace Enemy 
{
    public enum EnemyState
    {
        Idle,
        Walk,
        WalkToPlayer,
        Attack,
        Dead
    }

    public class EnemyAnimator : BaseAnimController
    {

        public EnemyState CurState { get; set; }
        public EnemyState PreviousState { get; set; }
        protected override int curState => (int)CurState;
        private Dictionary<EnemyState,int> animStateHashDic;
        
        public EnemyAnimator(BaseEnemy enemy) : base(enemy, enemy)
        {
        }
        
        protected override void SetStateDic()
        {
            animStateHashDic = new Dictionary<EnemyState, int>
            {
                {EnemyState.Idle, AnimaHash.state_Idle},
                {EnemyState.Walk, AnimaHash.state_Walk},
                {EnemyState.WalkToPlayer, AnimaHash.state_Walk},
                {EnemyState.Attack, AnimaHash.state_Attack},
                {EnemyState.Dead, AnimaHash.state_Dead}
               
            };
            
            CurState = EnemyState.Idle;
            AddLockState((int)EnemyState.Dead);
        }

        public void SetAnim(EnemyState state)
        {
            base.SetAnim((int)state);
        }

        protected override bool ForceChangeState(int newState)
        {
            switch ((EnemyState)newState)
            {
                case EnemyState.Dead:
                    return true;
                default:
                    return false;
            }
        }

        protected override void ChangeAnimState(int state)
        {
            PreviousState = CurState;
            CurState = (EnemyState) state;
            animator.Play(animStateHashDic[CurState],0,0);
        }
    }
    
}
