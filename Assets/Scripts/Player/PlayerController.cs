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
        public static PlayerController Instance;
        private PlayerAnimController animator { get;set; }
        private PlayerMove rbMove { get; set; }
        public InputActionPhase phase{ get; set; }
        public PlayerState PlayerState
        {
            get => animator.CurState;
            protected set =>animator.SetAnim(value);
        }

        // public bool CantChangeState => isDance || isGrab;
        public bool isGrab => PlayerState is PlayerState.Bala_1 or PlayerState.Bala_2 or PlayerState.Bala_3;
        public bool isRun => PlayerState is PlayerState.PickRun or PlayerState.Run;
        public bool isDance => PlayerState is PlayerState.Dance_1 or PlayerState.Dance_2 or PlayerState.Dance_3 or PlayerState.Dance_4 or PlayerState.Dance_5;
        
        protected List<PlayerState> danceStateList = new List<PlayerState>
        {
            PlayerState.Dance_1,
            PlayerState.Dance_2,
            PlayerState.Dance_3,
            PlayerState.Dance_4,
            PlayerState.Dance_5
        };

        // private GamePlayInput gamePlayInput;
        #region UnityAction

        protected void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            Instance = this;
            animator = new PlayerAnimController(this);
            rbMove = new PlayerMove(5, this);
        }

        #endregion

        #region Listener

        private void Update()
        {
            rbMove.Move(GamePlay_InputAction.moveDir);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed && (rbMove.CanMove || isDance || isGrab))
            {
                PlayerState = PlayerState.Run;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                PlayerState = PlayerState.Idle;    
            }
        }

        public void PressSpace(InputAction.CallbackContext context)
        {
            
        }
        
        private void Throw(InputAction.CallbackContext context)
        {
            
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

        }
        public virtual void AnimatorStateComplete()
        {
        
        }
        #endregion
    }

}
