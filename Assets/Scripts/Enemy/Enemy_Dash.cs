using item;
using Player;
using tool;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class Enemy_Dash : BaseEnemy
    {
        [SerializeField]
        protected float dashTime = 1;
        [SerializeField]
        protected float alertTime = 1;
        [SerializeField]
        protected float dashSpeed = 5;

        
        protected bool isEnterAttack = false;

        protected Vector3 direction;
        
        protected DashMove dashMove;
        protected override void SetMove()
        {
            dashMove = new DashMove(this,enemyParameter.Speed,this);
            enemyMove = dashMove;
        }
        
        protected override void EnemyUpdate()
        {
            if(CurState == EnemyState.Dead)
                return;
            if (isEnterAttack)
            {
                if (CurState == EnemyState.Attack)
                {
                    dashMove.DirMove(direction,dashSpeed);
                    AttackTrigger();
                }
            }
            else if (enemyAttacker.CanAttack)
            {
                EnemyAttack();
            }
            else
            {
                EnemyPatrol();
            }
            
        }
        
        protected override void EnemyAttack()
        {
            if (!isEnterAttack)
            {
                direction = (playerPos - enemyPosition).normalized;
                dashMove.faceDir.FaceToTarget(playerPos);
                CurState = EnemyState.Alert;
                isEnterAttack = true;
            }
        }

        
        protected void DashComplete()
        {
            CurState = EnemyState.Recover;
        }
        public override void AnimatorStateEnter()
        {
            switch (CurState)
            {
                case EnemyState.Alert:
                    this.DelayExecute(alertTime, () =>
                    {
                        CurState = EnemyState.Attack;
                    });
                    break;
                case EnemyState.Attack:
                    enemyAttacker.AttackEnterCd();
                    this.DelayExecute(dashTime,DashComplete);
                    break;
                case EnemyState.Dead:
                    FxPlayer.PlayFx("Fx_EnemyDeath", enemyPosition);
                    break;
            }
        }

        public override void AnimatorStateComplete()
        {
            switch (CurState)
            {
                case EnemyState.Alert:
                    break;
                case EnemyState.Recover:
                    CurState = EnemyState.Idle;
                    isEnterAttack = false;
                    break;
                case EnemyState.Attack:
                    break;
                case EnemyState.Dead:
                    CurState = EnemyState.Idle;
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
    
}
