using System;
using data;
using other;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace game
{
    public class ClickObj : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUp();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {         
            OnClick();
            // GameCursor.Instance.ChangeCurrentClickEmpty();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnter();
         
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExit();
         
        }

        protected virtual void OnClick()
        {
            
        }
        protected virtual void OnEnter()
        {
            
        }
        
        protected virtual void OnExit()
        {
            
        }
        
        protected virtual void OnDown()
        {
            
        }
        
        protected virtual void OnUp()
        {
            
        }
    }
    

}