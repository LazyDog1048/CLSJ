
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace data
{
    public enum PressState
     {
         NotClick,
         Down,
         Pressing,
         ShortPressed,
         LongPressed,
         Up
     }

     public class ActionThing
     {
         public readonly InputAction action;
         private readonly UnityEvent<InputAction.CallbackContext> KeySateChange;

         // public ActionThing(string actionName,InputMapType mapType)
         public ActionThing(InputAction action)
         {
             this.action = action;
             KeySateChange = new UnityEvent<InputAction.CallbackContext>();             
             this.action.started += CallBack;
             this.action.performed += CallBack;
             this.action.canceled += CallBack;
             this.action.Enable();
         }

         public void AddListener(UnityAction<InputAction.CallbackContext> action)
         {
             KeySateChange.AddListener(action);
         }
         
         public void RemoveListener(UnityAction<InputAction.CallbackContext> action)
         {
             KeySateChange.RemoveListener(action);
         }
         public void RemoveAllListeners()
         {
             KeySateChange.RemoveAllListeners();
             Enable(false);
         }
         public void Enable(bool enable)
         {
             if (enable)
             {
                 action.Enable();
             }
             else
             {
                 action.Disable();
             }
         }
         private void CallBack(InputAction.CallbackContext context)
         {
             KeySateChange?.Invoke(context);
         }

         public string GetBind(string controlSchemeType)
         {
             var defaultBinding = action.bindings[0];
             foreach (var binding in action.bindings)
             {
                 if (binding.groups.Contains(controlSchemeType))
                 {
                     defaultBinding = binding;
                     break;
                 }
             }
             return defaultBinding.path.Split('/')[1];
         }

     }
}

