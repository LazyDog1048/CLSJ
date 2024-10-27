using UnityEngine;

namespace Enemy
{
    public class Enemy_Zombie : Enemy_Dash
    {
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
            if (!isEnterAttack)
            {
                direction = (playerPos - centerPosition).normalized;
                dashMove.faceDir.FaceToTarget(playerPos);
                CurState = EnemyState.Alert;
                isEnterAttack = true;
            }
        }
    }
    
}
