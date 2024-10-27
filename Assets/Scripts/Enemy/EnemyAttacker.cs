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

       
        public override void Reset()
        {
            currentHp = maxHp;
        }
        
        public bool CheckWatchPlayer()
        {
            float distance = Vector2.Distance(PlayerController.Instance.transform.position, _enemy.centerPosition);
            if(distance > enemyParameter.FindRange/2)
                return false;
            
            Vector2 dir = (PlayerController.Instance.transform.position - _enemy.centerPosition).normalized;
            
            var ray = Physics2D.Raycast(_enemy.centerPosition,dir, distance,LayerMask.GetMask("Wall"));
            return ray.collider == null;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
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

    }
    
}
