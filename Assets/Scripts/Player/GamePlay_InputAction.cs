using data;
using game.manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace so
{
    // [CreateAssetMenu(fileName = "InputAction", menuName = "ScriptableObjects/InputAction")]
    public class GamePlay_InputAction : BaseInputAction<GamePlay_InputAction>
    {
        private bool uiBlock;
        public override bool enable =>base.enable && !uiBlock;
        private ActionThing move;
        private ActionThing cursorMove;
        private ActionThing pressShift;
        private ActionThing pressE;
        private ActionThing pressQ;
        private ActionThing pressF;
        
        public bool isPressShift => pressShift.action.IsPressed();
        public void UiBlock(bool block)
        {
            uiBlock = block;
            CheckEnable();
        }
        
        public static Vector2 moveDir => Instance.actionMap["Move"].ReadValue<Vector2>();

        public static InputActionMap GetActionMap(InputMapType inputMapType)
        {
            return InputManager.Instance.inputActionAsset.FindActionMap(inputMapType.ToString());
        }
        protected override void RegisterAction()
        {
            actionMap = GetActionMap(InputMapType.GamePlay);
            Enable(true);
            move = AddAction("Move");
            cursorMove = AddAction("CursorMove");
            pressShift = AddAction("PressShift");
            pressE = AddAction("PressE");
            pressQ = AddAction("PressQ");
            pressF = AddAction("PressF");
        }

        public void PlayerRegisterAction(UnityAction<InputAction.CallbackContext> moveEvent,
                                         UnityAction<InputAction.CallbackContext> cursorMoveEvent,
                                         UnityAction<InputAction.CallbackContext> pressShiftEvent,
                                         UnityAction<InputAction.CallbackContext> pressEEvent,
                                         UnityAction<InputAction.CallbackContext> pressQEvent,
                                         UnityAction<InputAction.CallbackContext> pressFEvent)
        {
            move?.AddListener(moveEvent);
            cursorMove?.AddListener(cursorMoveEvent);
            pressShift?.AddListener(pressShiftEvent);
            pressE?.AddListener(pressEEvent);
            pressQ?.AddListener(pressQEvent);
            pressF?.AddListener(pressFEvent);
        }


        public override void DisposeInputAction()
        {
            base.DisposeInputAction();
            move?.RemoveAllListeners();
            cursorMove?.RemoveAllListeners();
            pressShift?.RemoveAllListeners();
            pressE?.RemoveAllListeners();
            pressQ?.RemoveAllListeners();
            pressF?.RemoveAllListeners();
        }

    }
    
}
