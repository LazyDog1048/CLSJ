using plug;
using UnityEngine;

namespace Enemy 
{
    public class EnemyMove : Moveable
    {
        public override bool CanMove => base.CanMove && (enemy.CurState is EnemyState.Idle or EnemyState.Walk or EnemyState.WalkToPlayer);

        private BaseEnemy enemy;
        public EnemyMove(BaseEnemy enemy,float Speed, MonoBehaviour mono) : base(Speed, mono)
        {
            this.enemy = enemy;
        }
        
        protected override void DoMove(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                target, MoveSpeed* Time.deltaTime);
            faceDir.FaceToTarget(target);
        }

    }    
}

