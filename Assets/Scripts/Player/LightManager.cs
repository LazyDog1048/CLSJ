using other;
using Player;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace game
{
    public class LightManager : Mono_Singleton<LightManager>
    {
        [Header("Global Light")]
        [SerializeField]
        private float globalLightIntensity =0.2f;
        
        [Header("Player FlashLight")]
        [SerializeField]
        int rayCount = 10;
        [SerializeField]
        public float flashLightInner = 10;
        [SerializeField]
        public float flashLightOuter = 11;
        [SerializeField]
        public float innerAngle = 80;
        [SerializeField]
        public float outerAngle = 90;
        
        [Header("Player Lantern")]
        [SerializeField]
        public float lanternLightInner = 2.5f;
        [SerializeField]
        public float lanternLightOuter = 3.5f;
        public Light2D globalLight { get;private set; }
        public PlayerFlashlight playerFlashLight { get;private set; }
        public PlayerLantern playerLantern { get;private set; }
        public PlayerLightController playerLightController { get;private set; }
        
        protected override void Awake()
        {
            base.Awake();
            globalLight = GetComponentInChildren<Light2D>();
            globalLight.intensity = globalLightIntensity;
            
            playerFlashLight = PlayerFlashlight.Instance;
            playerLantern = PlayerLantern.Instance;
            playerLightController = PlayerLightController.Instance;
            
            playerFlashLight.SetLight(flashLightInner,flashLightOuter,innerAngle,outerAngle,rayCount);
            playerLantern.SetLight(lanternLightInner,lanternLightOuter);
            
        }
        
        
        
        
    }
    
}
