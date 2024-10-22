using System.Collections.Generic;
using other;
using Player;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace game
{
    public class PlayerFlashlight : Mono_Singleton<PlayerFlashlight>
    {
        private int rayCount = 10;

        private float innerLength = 10;
        private float outerLength = 11;
        private float innerAngle = 90;
        private float outerAngle = 80;

        private EdgeCollider2D edgeCollider2D;
        private Light2D light2D;

        public float curOuterAngle { get;private set; }
        public float curOuter{ get;private set; }


        public void SetLight(float innerLength,float outerLength,float innerAngle,float outerAngle,int rayCount)
        {
            light2D = GetComponent<Light2D>();
            edgeCollider2D = GetComponent<EdgeCollider2D>();
            
            this.innerLength = innerLength;
            this.outerLength = outerLength;
            this.innerAngle = innerAngle;
            this.outerAngle = outerAngle;
            
            this.rayCount = rayCount;
            GetPoints(innerAngle,outerLength);
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
            curOuter = outerLength*2;
            curOuterAngle = outerAngle/2;
                
            
            
            light2D.pointLightInnerAngle = innerAngle / 2;
            light2D.pointLightOuterAngle = outerAngle / 2;
            
            light2D.pointLightInnerRadius = innerLength * 2;
            light2D.pointLightOuterRadius = outerLength * 2;
            GetPoints(curOuterAngle,curOuter);
        }
        
        public void NormalMode()
        {
            curOuter = outerLength;
            curOuterAngle = outerAngle;
            
            light2D.pointLightInnerAngle = innerAngle;
            light2D.pointLightOuterAngle = outerAngle;
            
            light2D.pointLightInnerRadius = innerLength;
            light2D.pointLightOuterRadius = outerLength;
            GetPoints(curOuterAngle,curOuter);
            
        }

     
    }
    
}
