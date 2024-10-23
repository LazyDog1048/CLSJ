using UnityEngine;

namespace Enemy
{
    public class CowMove : EnemyMove 
    {
        protected override bool isMoveState => base.isMoveState ||  enemy.CurState is EnemyState.Attack;

        public CowMove(BaseEnemy enemy, float Speed, MonoBehaviour mono) : base(enemy, Speed, mono)
        {
        }
        
        public void DirMove(Vector3 dir,float speed)
        {
            if(!CanMove)
                return;
            DoDirMove(dir,speed);
        }
        protected void DoDirMove(Vector3 dir,float speed)
        {
            Vector3 step = transform.position + dir * speed * Time.deltaTime;
            rb.MovePosition(step);
            faceDir.FaceToTarget(step);
        }

    }
    
}
