using Player;
using UnityEngine;

namespace plug
{
    public class PlayerMove : Moveable
    {
        public override bool CanMove => base.CanMove && player.PlayerState is PlayerState.Idle or PlayerState.Run;
        
        private readonly PlayerController player;
        protected readonly Rigidbody2D rb;
        private Vector3 offset;
        private bool isFastRun;
        public PlayerMove(float Speed, PlayerController go) : base(Speed, go)
        {
            player = go;
            rb = mono.GetComponent<Rigidbody2D>();
            offset = new Vector3(-0.5f, 0, 0);
        }
        
        protected override void DoMove(Vector3 target)
        {
            rb.MovePosition(rb.position + (Vector2)target * moveSpeed.FinalValue * Time.fixedDeltaTime);
        }

        // public void StartPlayRunFx()
        // {
        //     if(isFastRun)
        //         return;
        //     isFastRun = true;
        //     player.LoopDelayExecute(0.1f,()=>!player.isRun,FastRunFx,FastRunComplete);
        // }
        // public void FastRunFx()
        // {
        //     // var FeetPos = player.FeetPos + (faceDirable.isFaceToRight ? offset : -offset);
        //     FxPlayer.PlayFx("Fx_FastRun", player.FeetPos).FlipX(!faceDirable.isFaceToRight);
        // }
        
        private void FastRunComplete()
        {
            isFastRun = false;
        }
    }
    
}
