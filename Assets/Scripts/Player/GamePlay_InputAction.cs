using data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace game
{
    // [CreateAssetMenu(fileName = "InputAction", menuName = "ScriptableObjects/InputAction")]
    public class GamePlay_InputAction : BaseInputAction<GamePlay_InputAction>
    {
        private bool uiBlock;
        public override bool enable =>base.enable && !uiBlock;
        private ActionThing move;
        private ActionThing cursorMove;
        private ActionThing rightMouse;
        private ActionThing pressShift;
        private ActionThing pressTab;
        private ActionThing pressR;
        private ActionThing pressE;
        private ActionThing pressQ;
        private ActionThing pressF;
        
        private ActionThing press1;
        private ActionThing press2;
        private ActionThing press3;
        private ActionThing press4;
        private ActionThing press5;
        private ActionThing press6;
        private ActionThing press7;
        
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
            rightMouse = AddAction("RightMouse");
            pressShift = AddAction("PressShift");
            pressTab = AddAction("PressTab");
            pressR = AddAction("PressR");
            pressE = AddAction("PressE");
            pressQ = AddAction("PressQ");
            pressF = AddAction("PressF");
            
            press1 = AddAction("Press1");
            press2 = AddAction("Press2");
            press3 = AddAction("Press3");
            press4 = AddAction("Press4");
            press5 = AddAction("Press5");
            press6 = AddAction("Press6");
            press7 = AddAction("Press7");
        }

        public void PlayerRegisterAction(UnityAction<InputAction.CallbackContext> moveEvent,
                                         UnityAction<InputAction.CallbackContext> cursorMoveEvent,
                                         UnityAction<InputAction.CallbackContext> rightMouseEvent,
                                         UnityAction<InputAction.CallbackContext> pressShiftEvent,
                                         UnityAction<InputAction.CallbackContext> pressTabEvent,
                                         UnityAction<InputAction.CallbackContext> pressREvent,
                                         UnityAction<InputAction.CallbackContext> pressEEvent,
                                         UnityAction<InputAction.CallbackContext> pressQEvent,
                                         UnityAction<InputAction.CallbackContext> pressFEvent)
        {
            move?.AddListener(moveEvent);
            cursorMove?.AddListener(cursorMoveEvent);
            rightMouse?.AddListener(rightMouseEvent);
            pressShift?.AddListener(pressShiftEvent);
            pressTab?.AddListener(pressTabEvent);
            pressR?.AddListener(pressREvent);
            pressE?.AddListener(pressEEvent);
            pressQ?.AddListener(pressQEvent);
            pressF?.AddListener(pressFEvent);
        }

        public void PlayerRegisterNumAction(UnityAction<InputAction.CallbackContext> press1Event,
                                         UnityAction<InputAction.CallbackContext> press2Event,
                                         UnityAction<InputAction.CallbackContext> press3Event,
                                         UnityAction<InputAction.CallbackContext> press4Event,
                                         UnityAction<InputAction.CallbackContext> press5Event,
                                         UnityAction<InputAction.CallbackContext> press6Event,
                                         UnityAction<InputAction.CallbackContext> press7Event)
        {
            press1?.AddListener(press1Event);
            press2?.AddListener(press2Event);
            press3?.AddListener(press3Event);
            press4?.AddListener(press4Event);
            press5?.AddListener(press5Event);
            press6?.AddListener(press6Event);
            press7?.AddListener(press7Event);
        }

        public override void DisposeInputAction()
        {
            base.DisposeInputAction();
            move?.RemoveAllListeners();
            cursorMove?.RemoveAllListeners();
            rightMouse?.RemoveAllListeners();
            pressShift?.RemoveAllListeners();
            pressTab?.RemoveAllListeners();
            pressR?.RemoveAllListeners();
            pressE?.RemoveAllListeners();
            pressQ?.RemoveAllListeners();
            pressF?.RemoveAllListeners();
            
            press1?.RemoveAllListeners();
            press2?.RemoveAllListeners();
            press3?.RemoveAllListeners();   
            press4?.RemoveAllListeners();
            press5?.RemoveAllListeners();
            press6?.RemoveAllListeners();
            press7?.RemoveAllListeners();
        }

    }
    
}
