using Cinemachine;
using other;
using UnityEngine;

namespace game
{
    public class CameraManager : KeepMonoSingleton<CameraManager>
    {
        [HideInInspector]
        public Camera mainCamera;

        public CinemachineSwitcher cinemachineSwitcher{ get;private set; }

        public CinemachineVirtualCamera cinemachineVirtualCamera { get;private set; }
        protected override void KeepInit()
        {
            mainCamera = transform.Find("MainCamera").GetComponent<Camera>();
            cinemachineSwitcher = GetComponent<CinemachineSwitcher>();
            cinemachineVirtualCamera = mainCamera.GetComponent<CinemachineVirtualCamera>();
            transform.position = new Vector3(0, 0, -10);
        }

        public void Load()
        {
            cinemachineSwitcher.LoadCM_Level_1920(transform.Find("CM_1920"));
        }

    }
    

}

