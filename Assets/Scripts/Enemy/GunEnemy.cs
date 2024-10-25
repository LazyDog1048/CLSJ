using data;
using EquipmentSystem;
using tool;
using UnityEngine;

namespace Enemy
{
    public class GunEnemy : BaseEnemy
    {
        [SerializeField]
        private float range = 10;
        // [SerializeField]
        // private Bullet_Data = 10;
        
        private Transform hand;
        private Transform gun;
        private Transform shotPoint;
        protected override void OnAwake()
        {
            base.OnAwake();
            hand = transform.Find("Hand");
            gun = hand.Find("Gun");
            shotPoint = gun.Find("ShotPoint");
        }
        
        protected override void EnemyAttack()
        {
            var angle = GetAngle.Angle(playerPos, transform.position);
            hand.rotation = Quaternion.Euler(0, 0, angle);
            
            if (transform.DisLongerThan(playerPos, enemyParameter.AttackRange))
            {
                CurState = EnemyState.WalkToPlayer;
                enemyMove.Move(playerPos);
            }
            else
            {
                CurState = enemyAttacker.isAttackCd? EnemyState.Idle : EnemyState.Attack;
            }
        }
        
        protected override void AttackTrigger()
        {
            var dir = (playerPos - shotPoint.position).normalized;
            var angle = GetAngle.Angle(playerPos, shotPoint.position);
            var shotPos = shotPoint.position.GetDirDistance(playerPos,1);

            // var afterDir = dir.Rota2DAxis(Random.Range(-range, range));
            // Bullet baseBullet = Load<Bullet>(bulletData);
            // if (!transform.DisLongerThan(playerPos, 1f))
            // {
            //     PlayerController.Instance.playerAttacker.TakeDamage(this);
            // }
        }
    }
    
}
