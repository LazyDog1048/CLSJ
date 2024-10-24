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
        private float normalInnerLength = 10;
        [SerializeField]
        private float normalOuterLength = 11;
        [SerializeField]
        private float normalInnerAngle = 40;
        [SerializeField]
        private float normalOuterAngle = 45;

        [SerializeField]
        private float aimInnerLength = 20;
        [SerializeField]
        private float aimOuterLength = 22;
        [SerializeField]
        private float aimInnerAngle = 20;
        [SerializeField]
        private float aimOuterAngle = 25;

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
            
            playerFlashLight.SetLight(normalInnerLength,normalOuterLength,normalInnerAngle,normalOuterAngle,aimInnerLength,aimOuterLength,aimInnerAngle,aimOuterAngle,rayCount);
            playerLantern.SetLight(lanternLightInner,lanternLightOuter);
            
        }
        
        
        
        
    }
    
}
