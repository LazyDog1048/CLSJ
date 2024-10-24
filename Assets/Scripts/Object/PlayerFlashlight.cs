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

        private float normalInnerLength = 10;
        private float normalOuterLength = 11;
        private float normalInnerAngle = 90;
        private float normalOuterAngle = 80;

        private float aimInnerLength = 20;
        private float aimOuterLength = 22;
        private float aimInnerAngle = 45;
        private float aimOuterAngle = 40;
        
        private EdgeCollider2D edgeCollider2D;
        private Light2D light2D;

        public float curOuterAngle { get;private set; }
        public float curOuter{ get;private set; }


        public void SetLight(float normalInnerLength,float normalOuterLength,float normalInnerAngle,float normalOuterAngle,float aimInnerLength,float aimOuterLength,float aimInnerAngle,float aimOuterAngle,int rayCount)
        {
            light2D = GetComponent<Light2D>();
            edgeCollider2D = GetComponent<EdgeCollider2D>();
            
            this.normalInnerLength = normalInnerLength;
            this.normalOuterLength = normalOuterLength;
            this.normalInnerAngle = normalInnerAngle;
            this.normalOuterAngle = normalOuterAngle;
            
            this.aimInnerLength = aimInnerLength;
            this.aimOuterLength = aimOuterLength;
            this.aimInnerAngle = aimInnerAngle;
            this.aimOuterAngle = aimOuterAngle;
            
            this.rayCount = rayCount;
            GetPoints(normalInnerAngle,normalOuterLength);
            NormalMode();
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
            curOuter = aimOuterLength;
            curOuterAngle = aimOuterAngle;



            light2D.pointLightInnerAngle = aimInnerAngle;
            light2D.pointLightOuterAngle = aimOuterAngle;
            
            light2D.pointLightInnerRadius = aimInnerLength;
            light2D.pointLightOuterRadius = aimOuterLength;
            GetPoints(curOuterAngle,curOuter);
        }
        
        public void NormalMode()
        {
            curOuter = normalOuterLength;
            curOuterAngle = normalOuterAngle;
            
            light2D.pointLightInnerAngle = normalInnerAngle;
            light2D.pointLightOuterAngle = normalOuterAngle;
            
            light2D.pointLightInnerRadius = normalInnerLength;
            light2D.pointLightOuterRadius = normalOuterLength;
            GetPoints(curOuterAngle,curOuter);
            
        }

     
    }
    
}
