using UnityEngine;
using UnityEngine.InputSystem;

namespace ui
{
    public enum PanelType
    {
        Normal,
        LockPlayer,
        PauseGame
    }
    public class BasePanel
    {
        public GameObject obj { get; private set; }
        public Transform transform { get; private set; }
        public RectTransform rectTransform { get; private set; }
        public string panelTypeString { get; protected set; }
        protected BasePanel(Transform trans)
        {
            transform = trans;
            obj = trans.gameObject;
            rectTransform = trans.GetComponent<RectTransform>();
            panelTypeString = GetType().Name;
        }

        public virtual void SetActive(bool active)
        {
            obj.SetActive(active);
        }
        
        public virtual void RemoveListener()
        {
                
        }
        
        public virtual void MouseMove(InputAction.CallbackContext context)
        {
            
        }
        
        public virtual void Confirm(InputAction.CallbackContext context)
        {
            
        }

   
        
        public virtual void Cancel(InputAction.CallbackContext context)
        {
            
        }
        
        public virtual void Move(InputAction.CallbackContext context,Vector2 moveDir)
        {
            
        }
        
        public static void ResetRect(RectTransform rectTransform)
        {
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;
            
            rectTransform.pivot = Vector2.zero;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
        }
    }
}
