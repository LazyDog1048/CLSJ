using System;
using game;
using Player;
using plug;
using UnityEngine;

namespace Enemy
{
    public class BaseEnemy : MonoPoolObj
    {
        [SerializeField]
        public LayerMask lightMask;
        
        public EnemySoData enemySoData;
        
        private EnemyParameter enemyParameter;
        
        private EnemyMove enemyMove;
        private Vector3 enemyPosition=>transform.position;
        public override string poolId => enemySoData.Name;
        private bool playerEnter;
        protected override void OnAwake()
        {
            // enemyParameter = new EnemyParameter(enemySoData);
            // enemyMove = new EnemyMove(enemyParameter.Speed,this);
        }

        private void Update()
        {
            if (playerEnter && !CheckPlayerTouch())
            {
                CheckPlayer();
            }
            
        }


        private void CheckPlayer()
        {
            
        }

        private bool CheckPlayerTouch()
        {
            Vector2 dir = (PlayerController.Instance.transform.position - enemyPosition).normalized;
            var ray = Physics2D.Raycast(enemyPosition,dir, enemyParameter.AttackRange,lightMask);
            return ray.collider != null;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            
        }
        
        
    }
    
}
