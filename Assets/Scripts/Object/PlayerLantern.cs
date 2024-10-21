using other;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace game
{
    public class PlayerLantern : Mono_Singleton<PlayerLantern>
    {
        private float outerLength;
        private float innerLength;
        
        private Light2D light2D;
        private CircleCollider2D circleCollider2D;
        protected override void Awake()
        {
            base.Awake();
            light2D = GetComponent<Light2D>();
            circleCollider2D = GetComponent<CircleCollider2D>();
        }
        
        public void SetLight(float innerLength,float outerLength)
        {
            this.innerLength = innerLength;
            this.outerLength = outerLength;
            light2D.pointLightOuterRadius = outerLength;
            light2D.pointLightInnerRadius = innerLength;
            circleCollider2D.radius = outerLength;
        }
        
    }
    
}
