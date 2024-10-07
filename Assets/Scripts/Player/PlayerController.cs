using System.Collections.Generic;
using game;
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
        private PlayerData playerData;

        public BulletData BulletData;
        public PlayerParameter playerParameter { get; set; }
        // [SerializeField]
        // public FieldOfView fieldOfView;
        public static PlayerController Instance;
        private PlayerAnimController animator { get;set; }
        public PlayerMove playerMove { get; set; }
        private PlayerStamina playerStamina { get; set; }
        public PlayerHand PlayerHand { get; set; }
        
        private Gun gun;
        public float angle => PlayerHand.angle;
        
        public bool isRun => PlayerState == PlayerState.Run;
        public InputActionPhase phase{ get; set; }
        
        private bool isPressShift;

        // public PlayerState previousState;
        public PlayerState PlayerState
        {
            get => animator.CurState;
            protected set
            {
                PlayerState previousState = PlayerState;
                animator.SetAnim(value);
                if(value == PlayerState && previousState != PlayerState)
                    StateChange(previousState,PlayerState);
            }
        }


        // private GamePlayInput gamePlayInput;
        #region UnityAction

        protected void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            Instance = this;
            
            playerParameter = new PlayerParameter(playerData);
            animator = new PlayerAnimController(this);
            playerMove = new PlayerMove(this);
            playerStamina = new PlayerStamina(this);
            PlayerHand = new PlayerHand(this);
            gun = GetComponentInChildren<Gun>();
            GamePlay_InputAction.Instance.PlayerRegisterAction(OnMove,CursorMoveEvent,RightMouse,PressShift,PressE,PressQ,PressF);
            GamePlay_InputAction.Instance.ConfirmUiAction(LeftMouse);
        }

        #endregion

        #region Listener

        private void Update()
        {
            playerMove.Move(GamePlay_InputAction.moveDir);
            PlayerHand.UpdateFieldOfView();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed && playerMove.CanMove)
            {
                PlayerState = isPressShift && playerStamina.CanRun ? PlayerState.Run : PlayerState.Walk;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                PlayerState = PlayerState.Idle;    
            }
        }

        private void CursorMoveEvent(InputAction.CallbackContext context)
        {
            PlayerHand.MouseMove(context);
        }
        
        private void LeftMouse(InputAction.CallbackContext context)
        {
            gun.GunFire(context);
        }
        
        private void RightMouse(InputAction.CallbackContext context)
        {
            PlayerHand.RightMouse(context);
        }
        
        private void PressShift(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                isPressShift = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                isPressShift = false;
            }
        }

        private void StateChange(PlayerState previousState,PlayerState curState)
        {
            if (curState == PlayerState.Run)
            {
                playerStamina.StartRunConsumeStamina();
            }
            else if(previousState == PlayerState.Run)
            {
                playerStamina.StopRunConsumeStamina();
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
            playerMove.AutoMove(targetPos,Complete);
        }

        public void PlayerRelease()
        {
            transform.SetParent(null);
            PlayerState = PlayerState.Idle;
        }

        #region IAnimController
        public virtual void AnimatorStateEnter()
        {
            
        }
        public virtual void AnimatorStateComplete()
        {
        
        }
        #endregion
    }

}
