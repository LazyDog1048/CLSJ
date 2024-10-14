using other;
using UnityEngine;

namespace game
{
    public class CameraManager : KeepMonoSingleton<CameraManager>
    {
        [HideInInspector]
        public Camera mainCamera;
        protected override void KeepInit()
        {
            mainCamera = transform.Find("MainCamera").GetComponent<Camera>();   
            transform.position = new Vector3(0, 0, -10);
        }

    }
    

}

