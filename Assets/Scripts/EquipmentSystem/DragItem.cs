using tool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ui
{
    [RequireComponent(typeof(RectTransform))]
    public class DragItem : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        [HideInInspector]
        public RectTransform rectTransform;
        protected CanvasGroup canvasGroup;

        public UnityAction OnDragAction;
        public UnityAction OnBeginDragAction;
        public UnityAction OnEndDragAction;
        
        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }


        public virtual void OnDrag(PointerEventData eventData)
        {
            // rectTransform.anchoredPosition += eventData.delta/ LayerPanel.Instance.canvas.scaleFactor;
            rectTransform.anchoredPosition = UiTool.WorldToUiPos();
            OnDragAction?.Invoke();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.8f;
            OnBeginDragAction?.Invoke();
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            OnEndDragAction?.Invoke();
        }
    }
    
}
