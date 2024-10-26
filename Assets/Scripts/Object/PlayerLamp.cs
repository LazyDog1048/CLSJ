using EquipmentSystem;
using other;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace game
{
    public class PlayerLamp : Mono_Singleton<PlayerLamp>
    {
        private float outerLength;
        private float innerLength;
        
        
        private float lampOuterLength;
        private float lampInnerLength;
        
        private Light2D light2D;
        private CircleCollider2D circleCollider2D;
        
        
        public void SetLight(float innerLength,float outerLength,float lampInnerLength,float lampOuterLength)
        {
            light2D = GetComponent<Light2D>();
            circleCollider2D = GetComponent<CircleCollider2D>();
            this.innerLength = innerLength;
            this.outerLength = outerLength;
            this.lampInnerLength = lampInnerLength;
            this.lampOuterLength = lampOuterLength;
            light2D.pointLightInnerRadius = innerLength;
            light2D.pointLightOuterRadius = outerLength;
            circleCollider2D.radius = outerLength;
        }
        

        public void UseLamp(Lamp lamp)
        {
            light2D.pointLightInnerRadius = lampInnerLength;
            light2D.pointLightOuterRadius = lampOuterLength;
            circleCollider2D.radius = lampOuterLength;
        }
        
        
        public void Resume()
        {
            light2D.pointLightInnerRadius = innerLength;
            light2D.pointLightOuterRadius = outerLength;
            circleCollider2D.radius = outerLength;
        }
    }
    
}
