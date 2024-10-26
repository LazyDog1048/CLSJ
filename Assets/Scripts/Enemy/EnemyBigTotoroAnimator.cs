using game;
using UnityEngine;

namespace Enemy
{
    public class EnemyBigTotoroAnimator : EnemyAnimator
    {
        private int[] idle_Hash = new int[4];
        // private int[] idle_Hash = new int[4];
        public EnemyBigTotoroAnimator(BaseEnemy enemy) : base(enemy)
        {
            // AnimaHash.state_Attack_Font
        }
        
        protected override void SetStateDic()
        {
            base.SetStateDic();
            idle_Hash = new[]
            {
                AnimaHash.state_Idle_0,
                AnimaHash.state_Idle_1
            };
        }
    }
    
}
