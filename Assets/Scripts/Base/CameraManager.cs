using Cinemachine;
using other;
using Player;
using tool;
using ui;
using UnityEngine;

namespace game
{
    public class CameraManager : KeepMonoSingleton<CameraManager>
    {
        [HideInInspector]
        public Camera mainCamera;
        public Camera uiCamera;
        
        public CinemachineSwitcher cinemachineSwitcher{ get;private set; }

        public CinemachineVirtualCamera cinemachineVirtualCamera { get;private set; }
        protected override void KeepInit()
        {
            mainCamera = transform.Find("MainCamera").GetComponent<Camera>();
            cinemachineSwitcher = GetComponent<CinemachineSwitcher>();
            cinemachineVirtualCamera = mainCamera.GetComponent<CinemachineVirtualCamera>();
            mainCamera.depth = 0;
            uiCamera.clearFlags = CameraClearFlags.Depth;
            uiCamera.depth = 10;
            transform.position = new Vector3(0, 0, -10);
        }

        public void Load()
        {
            cinemachineSwitcher.LoadCM_Level_1920(transform.Find("CM_1920"));
        }

        public Vector3 PlayerPosToUiPos()
        {
            // return uiCamera.ScreenToWorldPoint(PlayerController.Instance.Head.position);
            return UiTool.WorldToUiPos(LayerPanel.Instance.rectTransform,uiCamera,PlayerController.Instance.Head.position);
        }
    }
    

}

