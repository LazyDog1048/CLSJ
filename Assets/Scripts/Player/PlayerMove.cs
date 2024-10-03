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
        
        public PlayerMove(PlayerController go) : base(go.playerParameter.speed, go)
        {
            player = go;
            rb = mono.GetComponent<Rigidbody2D>();
        }
        
        protected override void DoMove(Vector3 target)
        {
            rb.MovePosition(rb.position + (Vector2)target * moveSpeed.FinalValue * Time.fixedDeltaTime);
        }


        public void PlayerRunStart()
        {
            moveSpeed.ExtraRate += parameter.runRate;
        }
        
        public void PlayerRunComplete()
        {
            moveSpeed.ExtraRate -= parameter.runRate;
        }
        
    }
    
}
