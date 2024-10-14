using data;
using game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ui
{
    public class Ui_InputAction : BaseInputAction<Ui_InputAction>
    {
        private bool heroSkillBlock;
        public bool HeroSkillBlock 
        {
            get=>heroSkillBlock;
            set
            {
                heroSkillBlock = value;
                Instance.Enable(isEnable);
            }
        }
        
        
        public bool isEnable => !HeroSkillBlock;

        private ActionThing mousePosition;
        private ActionThing speedChange; 
        private ActionThing rightClick; 
        public static Vector2 moveDir => Instance.actionMap["Move"].ReadValue<Vector2>();
        public static Vector2 point => Instance.actionMap["Point"].ReadValue<Vector2>();


        public void OnlyClickEnable(bool enable)
        {
            // cancel.Enable(!enable);
        }
        public static InputActionMap GetActionMap(InputMapType inputMapType)
        {
            return InputManager.Instance.inputActionAsset.FindActionMap(inputMapType.ToString());
        }
        protected override void RegisterAction()
        {
            actionMap = GetActionMap(InputMapType.UI);
            actionMap.Enable();
            mousePosition = AddAction("Point");
            speedChange = AddAction("SpeedChange");
            rightClick = AddAction("RightClick");
        }


        public void SpeedUiRegisterAction(UnityAction<InputAction.CallbackContext> action)
        {
            speedChange?.AddListener(action);
        }
    
        public void MouseMoveRegisterAction(UnityAction<InputAction.CallbackContext> action)
        {
            mousePosition?.AddListener(action);
        }
        
        public void RightClickUiAction(UnityAction<InputAction.CallbackContext> action)
        {
            rightClick?.AddListener(action);
        }
        public override void DisposeInputAction()
        {
            base.DisposeInputAction();
            mousePosition?.RemoveAllListeners();
            speedChange?.RemoveAllListeners();
            rightClick?.RemoveAllListeners();
        }

        private void Click(InputAction.CallbackContext context)
        {
            // EventCenter.Instance.EventTrigger("Ui_Click", context);
        }
    }

}
