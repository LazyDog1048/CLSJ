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
        private ActionThing pick_put;
        private ActionThing throw_item;
        private ActionThing pressE;
        private ActionThing pressQ;
        private ActionThing pressF;
        
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
            pick_put = AddAction("Pick_Put");
            throw_item = AddAction("Throw");
            pressE = AddAction("Upgrade");
            pressQ = AddAction("Dance");
            pressF = AddAction("Grab");
        }

        public void PlayerRegisterAction(UnityAction<InputAction.CallbackContext> moveEvent,
                                         UnityAction<InputAction.CallbackContext> pickPutEvent,
                                         UnityAction<InputAction.CallbackContext> throwEvent,
                                         UnityAction<InputAction.CallbackContext> pressEEvent,
                                         UnityAction<InputAction.CallbackContext> pressQEvent,
                                         UnityAction<InputAction.CallbackContext> grabEvent)
        {
            move?.AddListener(moveEvent);
            pick_put?.AddListener(pickPutEvent);
            throw_item?.AddListener(throwEvent);
            pressE?.AddListener(pressEEvent);
            pressQ?.AddListener(pressQEvent);
            pressF?.AddListener(grabEvent);
        }


        public override void DisposeInputAction()
        {
            base.DisposeInputAction();
            move?.RemoveAllListeners();
            pick_put?.RemoveAllListeners();
            throw_item?.RemoveAllListeners();
            pressE?.RemoveAllListeners();
            pressQ?.RemoveAllListeners();
            pressF?.RemoveAllListeners();
        }

    }
    
}
