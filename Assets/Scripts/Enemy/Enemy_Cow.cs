using item;
using Player;
using tool;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class Enemy_Cow : BaseEnemy
    {
        [SerializeField]
        protected float dashTime = 1;
        [SerializeField]
        protected float dashSpeed = 5;

        
        protected bool isEnterAttack = false;

        protected Vector3 direction;
        
        CowMove cowMove;
        protected override void SetMove()
        {
            cowMove = new CowMove(this,enemyParameter.Speed,this);
            enemyMove = cowMove;
        }
        
        protected override void EnemyUpdate()
        {
            if(CurState == EnemyState.Dead)
                return;
            if (isEnterAttack)
            {
                if (CurState == EnemyState.Attack)
                {
                    cowMove.DirMove(direction,dashSpeed);
                    if (!transform.DisLongerThan(playerPos, 1f))
                    {
                        PlayerController.Instance.KnockBackPlayer(enemyPosition);
                    }
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
                    CurState = EnemyState.Attack;
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
