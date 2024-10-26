using EquipmentSystem;
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
        private FlashlightData flashlightData;

        [Header("Player Lantern")]
        [SerializeField]
        public float lanternLightInner = 2.5f;
        [SerializeField]
        public float lanternLightOuter = 3.5f;
        public float lampLightInner = 5;
        [SerializeField]
        public float lampLightOuter = 5.5f;
        
        public Light2D globalLight { get;private set; }
        public PlayerFlashlight playerFlashLight { get;private set; }
        public PlayerLamp PlayerLamp { get;private set; }
        public PlayerLightController playerLightController { get;private set; }
        
        protected override void Awake()
        {
            base.Awake();
            globalLight = GetComponentInChildren<Light2D>();
            globalLight.intensity = globalLightIntensity;
            
            playerFlashLight = PlayerFlashlight.Instance;
            PlayerLamp = PlayerLamp.Instance;
            playerLightController = PlayerLightController.Instance;
            
            playerFlashLight.SetLight(flashlightData,rayCount);
            PlayerLamp.SetLight(lanternLightInner,lanternLightOuter,lampLightInner,lampLightOuter);
            
        }
        
        
        
        
    }
    
}
