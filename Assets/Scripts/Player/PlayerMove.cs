using Player;
using UnityEngine;

namespace plug
{
    public class PlayerMove : Moveable
    {
        public override bool CanMove => base.CanMove && player.PlayerState is PlayerState.Idle or PlayerState.Walk or PlayerState.Run;
        
        private readonly PlayerController player;
        protected readonly Rigidbody2D rb;
        private PlayerParameter parameter => player.playerParameter;
        
        public override float MoveSpeed=> player.playerEquipment.playerMoveSpeed;
        public float BaseSpeed{ get;private set; }
        public int SpeedRate { get;private set; }
        public PlayerMove(PlayerController go) : base(go.playerParameter.speed, go)
        {
            player = go;
            BaseSpeed = go.playerParameter.speed;
            rb = mono.GetComponent<Rigidbody2D>();
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
        
    }
    
}
