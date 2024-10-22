using Cinemachine;
using other;
using UnityEngine.Events;

namespace game
{
    public class CameraShake
    {
        public static CameraShake Instance => CinemachineSwitcher.Instance.CameraShake;
        
        private CinemachineVirtualCamera cinemachineVirtualCamera;

        private CinemachineBasicMultiChannelPerlin cbmcp;
        private static float shakeInstensity = 2f;
        private static float shakeTime = 10f;

        public void SetVirtualCamera()
        {
            cinemachineVirtualCamera = CinemachineSwitcher.Instance.CurrentCamera;
            cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void ShakeCamera(float instensity,float time,UnityAction complete = null)
        {
            cbmcp.m_AmplitudeGain = instensity;
            cinemachineVirtualCamera.DelayExecute(time, () =>
            {
                StopShake();
                complete?.Invoke();
            });
        }
        
        public void ShakeCamera(float time,UnityAction complete = null)
        {
            cbmcp.m_AmplitudeGain = shakeInstensity;
            cinemachineVirtualCamera.DelayExecute(time, () =>
            {
                StopShake();
                complete?.Invoke();
            });
        }
        
        public void SmoothShakeCamera(float endInstensity,float time,UnityAction complete = null)
        {
            cbmcp.m_AmplitudeGain = shakeInstensity;
            cinemachineVirtualCamera.DelayExecute(time, () =>
            {
                StopShake();
                complete?.Invoke();
            });
        }
        
        public void ShakeCamera(UnityAction complete)
        {
            ShakeCamera(shakeTime, complete);
        }

        private void StopShake()
        {
            cbmcp.m_AmplitudeGain = 0;
        }
    }
    
}