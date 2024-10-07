using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace game
{
    public class PlayerFlashlight : MonoBehaviour
    {
        [SerializeField]
        int rayCount = 10;
        
        [SerializeField]
        int accuracy = 20;
        [SerializeField]
        protected LayerMask lightMask;
        public float inner = 10;
        public float outer = 11;
        public float angle = 90;
        private EdgeCollider2D edgeCollider2D;
        private Light2D light2D;

        private float curAngle;
        private float curInner;

        
        private void Awake()
        {
            light2D = GetComponent<Light2D>();
            edgeCollider2D = GetComponent<EdgeCollider2D>();
            GetPoints(angle,outer);
        }

        private void GetPoints(float fov,float length)
        {
            var pointList = new List<Vector2>();
            var tempAngle =90+ fov/2;
            var origin = transform.localPosition;
            
            pointList.Add(origin);
            float angleIncrease = fov / rayCount;
            for(int i=0;i<=rayCount;i++)
            {
                Vector3 vertex = origin +GetAngle.GetAngleFormVectorFloat(tempAngle) * length;
                pointList.Add(vertex);
                pointList.Add(origin);
                pointList.Add(vertex);
                tempAngle -= angleIncrease;
            }
            pointList.Add(origin);
            edgeCollider2D.SetPoints(pointList);
        }

        public void AimMode()
        {
            curAngle = angle/2;
            curInner = outer*2;
            light2D.pointLightInnerRadius = inner;
            light2D.pointLightOuterRadius = outer * 2;
            light2D.pointLightInnerAngle = curAngle;
            light2D.pointLightOuterAngle = curAngle;
            GetPoints(curAngle,curInner);
        }
        
        public void NormalMode()
        {
            curAngle = angle;
            curInner = outer;
            light2D.pointLightInnerRadius = inner;
            light2D.pointLightOuterRadius = outer;
            light2D.pointLightInnerAngle = angle;
            light2D.pointLightOuterAngle = angle;
            GetPoints(curAngle,curInner);
            
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
            var ray = Physics2D.Raycast(transform.position,dir, inner,lightMask);
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
                var ray = Physics2D.Raycast(origin,GetAngle.GetAngleFormVectorFloat(tempAngle), curInner, lightMask);
                Debug.DrawRay(origin,GetAngle.GetAngleFormVectorFloat(tempAngle)*curInner,Color.red);
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
