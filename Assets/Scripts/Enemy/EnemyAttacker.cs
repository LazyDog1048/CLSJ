using Enemy;
using EquipmentSystem;
using Player;
using UnityEngine;

namespace plug
{
    public class EnemyAttacker : AbstractComponent
    {
        public EnemyParameter enemyParameter;
        public bool playerEnter { get;private set; }
        public bool isAttackCd{ get;private set; }
        public int maxHp;
        public int currentHp;
        public bool CanAttack => playerEnter && CheckWatchPlayer();
        private BaseEnemy _enemy;
        public EnemyAttacker(BaseEnemy enemy,EnemyParameter enemyParameter) : base(enemy)
        {
            _enemy = enemy;
            this.enemyParameter = enemyParameter;
            Check2DRange check2DRange = transform.Find("FindRange").GetComponent<Check2DRange>();
            check2DRange.Init(enemyParameter.FindRange,OnTriggerEnter2D,OnTriggerExit2D);
            maxHp = enemyParameter.Health;
            currentHp = maxHp;

        }
        
        public bool CheckWatchPlayer()
        {
            Vector2 dir = (PlayerController.Instance.transform.position - transform.position).normalized;
            var ray = Physics2D.Raycast(transform.position,dir, enemyParameter.FindRange,LayerMask.GetMask("Wall"));
            return ray.collider == null;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log(col.name);
            if (col.tag.Equals("Player"))
            {
                playerEnter = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.tag.Equals("Player"))
            {
                playerEnter = false;
            }
        }

        public void AttackEnterCd()
        {
            isAttackCd = true;
            mono.DelayExecute(enemyParameter.AttackInterval, () =>
            {
                isAttackCd = false;
            });
        }

        public void TakeDamage(Bullet bullet)
        {
            currentHp -= bullet.gun.Damage;
            if (currentHp <= 0)
            {
                
            }
        }
    }
    
}
