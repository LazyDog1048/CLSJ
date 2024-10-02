using System.Collections.Generic;
using game.Other;
using plug;
using so;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    
    public class PlayerController : MonoBehaviour, IAnimController
    {
        [SerializeField]
        private float speed = 10;
        
        [SerializeField]
        public FieldOfView fieldOfView;
        public static PlayerController Instance;
        private PlayerAnimController animator { get;set; }
        private PlayerMove rbMove { get; set; }
        private Gun Gun { get; set; }
        
        public float angle => Gun.angle;
        public InputActionPhase phase{ get; set; }
        public PlayerState PlayerState
        {
            get => animator.CurState;
            protected set =>animator.SetAnim(value);
        }


        // private GamePlayInput gamePlayInput;
        #region UnityAction

        protected void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            Instance = this;
            animator = new PlayerAnimController(this);
            rbMove = new PlayerMove(speed, this);
            Gun = new Gun(this);
            GamePlay_InputAction.Instance.PlayerRegisterAction(OnMove, Gun.MouseMove,PressE,PressQ,PressF);
        }

        #endregion

        #region Listener

        private void Update()
        {
            rbMove.Move(GamePlay_InputAction.moveDir);
            Gun.UpdateFieldOfView();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed && rbMove.CanMove)
            {
                PlayerState = PlayerState.Run;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                PlayerState = PlayerState.Idle;    
            }
        }

        private void PressE(InputAction.CallbackContext context)
        {   
            
           
        }

        
        
        private void PressQ(InputAction.CallbackContext context)
        {
            
        }
        
        private void PressF(InputAction.CallbackContext context)
        {
            
        }
        #endregion


        public void PlayerStateChange(PlayerState state)
        {
            PlayerState = state;
        }
        public void PlayerWalkToTarget(Vector3 targetPos,UnityAction Complete)
        {
            rbMove.AutoMove(targetPos,Complete);
        }

        public void PlayerRelease()
        {
            transform.SetParent(null);
            PlayerState = PlayerState.Idle;
        }

        #region IAnimController
        public virtual void AnimatorStateEnter()
        {
            Debug.Log(PlayerState);
        }
        public virtual void AnimatorStateComplete()
        {
        
        }
        #endregion
    }

}
