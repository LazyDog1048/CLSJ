using plug;
using UnityEngine;

namespace Enemy 
{
    public class EnemyMove : Moveable
    {
        public override bool CanMove => base.CanMove && isMoveState;
        protected virtual bool isMoveState => enemy.CurState is EnemyState.Idle or EnemyState.PatrolIdle or EnemyState.Walk or EnemyState.WalkToPlayer or EnemyState.PatrolWalk;
        
        protected Rigidbody2D rb;
        protected BaseEnemy enemy;
        public EnemyMove(BaseEnemy enemy,float Speed, MonoBehaviour mono) : base(Speed, mono)
        {
            this.enemy = enemy;
            rb = mono.GetComponent<Rigidbody2D>();
        }
        
        protected override void DoMove(Vector3 target)
        {
            Vector3 step = Vector3.MoveTowards(transform.position, target, MoveSpeed* Time.deltaTime);
            rb.MovePosition(step);
            faceDir.FaceToTarget(step);
        }
        
       
        public void AddForce(Vector2 dir,float time)
        {
            float force = 1;
            rb.AddForce(dir * force,ForceMode2D.Impulse);
            MoveEnable(false);
            mono.DelayExecute(time,Stop);
        }
        
        public void Stop()
        {
            MoveEnable(true);
            rb.linearVelocity = Vector2.zero;
        }
    }    
}

