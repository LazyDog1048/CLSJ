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
        protected float recoverTime = 1;
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
                if ( CurState == EnemyState.Attack)
                {
                    dashMove.DirMove(direction,dashSpeed);
                    AttackTrigger();
                }
            }
            else if (enemyAttacker.CanAttack)
            {
                dashMove.faceDir.FaceToTarget(playerPos);
                SeenPlayer = true;
                
                EnemyAttack();
            }
            else if (SeenPlayer)
            {
                CurState = EnemyState.WaitIdle;
            }
            else
            {
                EnemyPatrol();
            }

        }
        
        protected override void EnemyAttack()
        {
            if (!isEnterAttack && !enemyAttacker.isAttackCd && !transform.DisLongerThan(playerPos, enemyParameter.AttackRange))
            {
                direction = (playerPos - centerPosition).normalized;
                dashMove.faceDir.FaceToTarget(playerPos);
                CurState = EnemyState.Alert;
                isEnterAttack = true;
            }
            else
            {
                CurState = EnemyState.WalkToPlayer;
                enemyMove.Move(playerPos);
            }
        }
        // protected override void EnemyAttack()
        // {
        //     if (!isEnterAttack)
        //     {
        //         direction = (playerPos - enemyPosition).normalized;
        //         dashMove.faceDir.FaceToTarget(playerPos);
        //         CurState = EnemyState.Alert;
        //         isEnterAttack = true;
        //     }
        // }


        private void DashComplete()
        {
            CurState = EnemyState.Recover;
            this.DelayExecute(recoverTime,RecoverComplete);
        }

        private void RecoverComplete()
        {
            isEnterAttack = false;
        }
        public override void AnimatorStateEnter()
        {
            switch (CurState)
            {
                case EnemyState.Alert:
                    enemyAttacker.AttackEnterCd();
                    this.DelayExecute(alertTime, () =>
                    {
                        CurState = EnemyState.Attack;
                    });
                    break;
                case EnemyState.Attack:
                    this.DelayExecute(dashTime,DashComplete);
                    break;
                case EnemyState.Recover:
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
                    break;
                case EnemyState.Attack:
                    break;
                case EnemyState.Dead:
                    CurState = EnemyState.Idle;
                    FxPlayer.PlayFx("Fx_EnemyDeath", centerPosition);
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
    
}
