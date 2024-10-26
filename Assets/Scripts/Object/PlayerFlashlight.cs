using System.Collections.Generic;
using EquipmentSystem;
using other;
using Player;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace game
{
    public class PlayerFlashlight : Mono_Singleton<PlayerFlashlight>
    {
        private int rayCount = 10;


        public FlashlightData currentFlashlightData;
        public FlashlightData baseFlashlightData;
        
        private EdgeCollider2D edgeCollider2D;
        private Light2D light2D;

        public float curOuterAngle { get;private set; }
        public float curOuter{ get;private set; }
        
        
        public void SetLight(FlashlightData flashlightData,int rayCount)
        {
            this.rayCount = rayCount;
            light2D = GetComponent<Light2D>();
            edgeCollider2D = GetComponent<EdgeCollider2D>();
            baseFlashlightData = flashlightData;
            currentFlashlightData = flashlightData;
            
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


        public void SwitchGun(PlayerGun gun)
        {
            // gameObject.SetActive(true);
            if (gun is Hand)
            {
                currentFlashlightData = baseFlashlightData;
            }
            else
            {
                currentFlashlightData = gun.gunData.flashlightData;
            }
            NormalMode();
        }


        public void AimMode()
        {
            curOuter = currentFlashlightData.aimOuterLength;
            curOuterAngle = currentFlashlightData.aimOuterAngle;

            light2D.pointLightInnerAngle = currentFlashlightData.aimInnerAngle;
            light2D.pointLightOuterAngle = currentFlashlightData.aimOuterAngle;
            
            light2D.pointLightInnerRadius = currentFlashlightData.aimInnerLength;
            light2D.pointLightOuterRadius = currentFlashlightData.aimOuterLength;
            GetPoints(currentFlashlightData.aimOuterAngle,currentFlashlightData.aimOuterLength);
        }
        
        public void NormalMode()
        {
            curOuter = currentFlashlightData.normalOuterLength;
            curOuterAngle = currentFlashlightData.normalOuterAngle;

            light2D.pointLightInnerAngle = currentFlashlightData.normalInnerAngle;
            light2D.pointLightOuterAngle = currentFlashlightData.normalOuterAngle;
            
            light2D.pointLightInnerRadius = currentFlashlightData.normalInnerLength;
            light2D.pointLightOuterRadius = currentFlashlightData.normalOuterLength;
            GetPoints(currentFlashlightData.normalOuterAngle,currentFlashlightData.normalOuterLength);
        }

    }
    
}
