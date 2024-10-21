using System;
using game;
using other;
using UnityEngine;

namespace Player
{
    public class PlayerLightController : Mono_Singleton<PlayerLightController>
    {
        [SerializeField]
        public int accuracy = 20;
        [SerializeField]
        public LayerMask lightMask;
        private float curOuter => playerFlashlight.curOuter;
        private float curAngle => playerFlashlight.curOuterAngle;

        PlayerFlashlight playerFlashlight;
        protected override void Awake()
        {
            base.Awake();
            playerFlashlight = transform.GetComponentInChildren<PlayerFlashlight>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var lightObj = col.GetComponentInParent<LightObj>();
            if (lightObj)
            {
                if(CheckLightTouch(col))
                    lightObj.LightEnter();
            }
        }
        
        private void OnTriggerStay2D(Collider2D col)
        {
            var lightObj = col.GetComponentInParent<LightObj>();
            if (lightObj)
            {
                // if(CheckLightTouch(col,lightObj))
                if(CheckLightTouch(col))
                    lightObj.LightEnter();
                else
                    lightObj.LightExit();
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            var lightObj = col.GetComponentInParent<LightObj>();
            if (lightObj)
            {
                lightObj.LightExit();
            }
        }

        private bool CheckLightTouch(Collider2D col)
        {
            Vector2 dir = (col.transform.position - transform.position).normalized;
            var ray = Physics2D.Raycast(transform.position,dir, curOuter,lightMask);
            if (ray.collider != null)
            {
                return col == ray.collider;
            }
            return true;
        }
     
        private bool AccuracyCheckLightTouch(Collider2D col)
        {
            var tempAngle = PlayerController.Instance.PlayerHand.angle - curAngle / 2 + curAngle;
            var origin = transform.position;
            float angleIncrease = curAngle / accuracy;
            
            for(int i=0;i<=accuracy;i++)
            {
                var ray = Physics2D.Raycast(origin,GetAngle.GetAngleFormVectorFloat(tempAngle), curOuter, lightMask);
                Debug.DrawRay(origin,GetAngle.GetAngleFormVectorFloat(tempAngle)*curOuter,Color.red);
                if (ray.collider != null && ray.collider == col)
                {
                    return true;
                }
                
                tempAngle -= angleIncrease;
            }

            return false;
        }
    }
    
}
