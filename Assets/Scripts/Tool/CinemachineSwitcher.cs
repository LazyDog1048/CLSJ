using System.Collections.Generic;
using Cinemachine;
using data;
using game;
using tool;
using UnityEngine;

namespace other
{
    public enum CinemachineType
    {
        CM_1920,
        // CM_GameShow,
    }
    
    public class CinemachineSwitcher : KeepMonoSingleton<CinemachineSwitcher>
    {
        [SerializeField]
        private PolygonCollider2D cameraCollider;
        [SerializeField]
        private CinemachineBlenderSettings settings;
        
        private CinemachineVirtualCamera currentCamera;

        private Dictionary<CinemachineType, CinemachineVirtualCamera> cameraDic;
        private CameraShake cameraShake;
        
        public CameraShake CameraShake=>cameraShake;
        public CinemachineVirtualCamera CurrentCamera => currentCamera;
        public CinemachineVirtualCamera this[CinemachineType type] 
            => cameraDic.ContainsKey(type) ? cameraDic[type] : LoadVirtualCamera(type);

        
        protected override void Awake()
        {
            cameraDic = new Dictionary<CinemachineType, CinemachineVirtualCamera>();
            cameraShake = new CameraShake();
        }

        private CinemachineVirtualCamera LoadVirtualCamera(CinemachineType type)
        {
            // Debug.Log(type);
            if (cameraDic.ContainsKey(type))
                return cameraDic[type];
            
            GameObject cameraObj = Loader.ResourceLoadGameObj<GameObject>($"{Data_Path.Re_CameraPath}/{type}");
            if (cameraObj == null)
                return null;
            cameraObj.transform.SetParent(transform);

            CinemachineVirtualCamera camera = cameraObj.GetComponent<CinemachineVirtualCamera>();
            camera.name = type.ToString();
            camera.Priority = 0;
            cameraDic.Add(type, camera);
            return camera;
        }
        
        public static void ChangeToCamera(CinemachineType cinemachineType)
        {
            Instance.SwitchToCamera(cinemachineType);
        }
        
        private void SwitchToCamera(CinemachineType cinemachineType)
        {
            if (!cameraDic.ContainsKey(cinemachineType))
                return;

            if(currentCamera!= null)
                currentCamera.Priority = 0;
            currentCamera = cameraDic[cinemachineType];
            currentCamera.Priority = 1;
        }
        //
        // public static void LoadCM_Player(Transform player)
        // {
        //     Instance.LoadVirtualCamera(CinemachineType.CM_Player);
        //     CinemachineBrain.SoloCamera = Instance[CinemachineType.CM_Player];
        //     Instance[CinemachineType.CM_Player].Follow = player;
        //     Instance[CinemachineType.CM_Player].LookAt = player;
        //     // Instance[CinemachineType.CM_Player].gameObject.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = Instance.cameraCollider;
        //     ChangeToCamera(CinemachineType.CM_Player);
        // }

        public override void LevelPrepare()
        {
            LoadCM_Level_1920();
            
        }
        public void LoadCM_Level_1920()
        {
            Instance.LoadVirtualCamera(CinemachineType.CM_1920);
            ChangeToCamera(CinemachineType.CM_1920);
            CinemachineBrain.SoloCamera = Instance[CinemachineType.CM_1920];
            Instance[CinemachineType.CM_1920].transform.position = new Vector3(0,0,-10);
            Instance[CinemachineType.CM_1920].gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = Instance.cameraCollider;
            cameraShake.SetVirtualCamera();
        }
    }
    
}
