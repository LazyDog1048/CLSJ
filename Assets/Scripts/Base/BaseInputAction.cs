using data;
using other;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace game
{
    public enum InputMapType
    {
        GamePlay,
        UI,
        Develop,
        Hero
    }
    
    public class BaseInputAction<TInput> : Class_Singleton<TInput> where TInput :new()
    {
        public virtual bool enable { get; protected set; }
        protected InputActionMap actionMap;
        protected ActionThing confirm;
        protected ActionThing cancel;
        protected override void Init()
        {
            base.Init();
            RegisterAction();
            confirm = AddAction("Confirm");
            cancel = AddAction("Cancel");
        }

        public void Enable(bool enable)
        {
            // Debug.Log($"{GetType()}  {enable}");
            if(actionMap == null)
                return;
            this.enable = enable;
            CheckEnable();
        }

        protected void CheckEnable()
        {
            if(actionMap == null)
                return;
            
            if (enable)
            {
                actionMap.Enable();
            }
            else
            {
                actionMap.Disable();
            }
        }

        protected ActionThing AddAction(string actionName)
        {
            return InputManager.Instance.AddAction($"{actionMap.name}_{actionName}",actionMap[actionName]);
        }
        protected virtual void RegisterAction()
        {
            
        }
        
        public void Mouse_LeftButton(UnityAction<InputAction.CallbackContext> action)
        {
            cancel?.AddListener(action);
        }
        
        public void ConfirmUiAction(UnityAction<InputAction.CallbackContext> action)
        {
            confirm?.AddListener(action);
        }

        public virtual void CancelUiAction(UnityAction<InputAction.CallbackContext> action)
        {
            cancel?.AddListener(action);
        }
        
        public virtual void EnableConfirmAndCancel(bool enable)
        {
            confirm = InputManager.GetKeyThing("Confirm");
            cancel = InputManager.GetKeyThing("Cancel");
        }

        public virtual void DisposeInputAction()
        {
            confirm?.RemoveAllListeners();
            cancel?.RemoveAllListeners();
        }
    }

    
}
