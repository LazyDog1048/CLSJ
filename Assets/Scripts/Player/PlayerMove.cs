using Player;
using UnityEngine;

namespace plug
{
    public class PlayerMove : Moveable
    {
        public override bool CanMove => base.CanMove && player.isPressMove && player.PlayerState is PlayerState.Idle or PlayerState.Walk or PlayerState.Run;
        
        private readonly PlayerController player;
        protected readonly Rigidbody2D rb;
        private PlayerParameter parameter => player.playerParameter;
        protected bool isKnockBack;
        public override float MoveSpeed=> player.playerEquipment.playerMoveSpeed;
        public float BaseSpeed{ get;private set; }
        public int SpeedRate { get;private set; }
        public PlayerMove(PlayerController go) : base(go.playerParameter.speed, go)
        {
            player = go;
            BaseSpeed = go.playerParameter.speed;
            rb = mono.GetComponent<Rigidbody2D>();
            isKnockBack = false;
        }

        protected override void DoMove(Vector3 target)
        {
            rb.MovePosition(rb.position + (Vector2)target * MoveSpeed * Time.fixedDeltaTime);
        }


        public void PlayerRunStart()
        {
            SpeedRate += parameter.runRate;
        }
        
        public void PlayerRunComplete()
        {
            SpeedRate -= parameter.runRate;
        }
        
        public void AddForce(Vector3 targetPos,float time)
        {
            if(isKnockBack)
                return;
            
            Vector2 dir = (transform.position - targetPos).normalized;
            if (50.RandomBy100Percent())
                dir = dir.Rota2DAxis(Random.Range(30, 60));
            else
                dir = dir.Rota2DAxis(Random.Range(-30, -60));
            
            isKnockBack = true;
            MoveEnable(false);
            float force = 10;
            rb.AddForce(dir * force,ForceMode2D.Impulse);
            mono.DelayExecute(time,Stop);
        }
        
        public void Stop()
        {
            isKnockBack = false;
            MoveEnable(true);
            rb.linearVelocity = Vector2.zero;
        }
    }
    
}
