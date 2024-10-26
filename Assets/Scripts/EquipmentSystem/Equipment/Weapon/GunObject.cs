using EquipmentSystem;
using game;
using GridSystem;
using item;
using other;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Player
{
    public class GunObject : GridGameObject,IAnimatorController
    {
        [SerializeField]
        protected Light2D fireLight;
        protected GunData gunData;
        protected GunAnimatorController gunAnimatorController;
        public GunState CurState
        {
            get => gunAnimatorController.CurState;
            set => gunAnimatorController.SetAnim(value);
        }

        public void Init()
        {
            gunAnimatorController = new GunAnimatorController(this);
        }

        public virtual void ReloadGun(GunData gunData)
        {
            this.gunData = gunData;
            gridObjectSo = gunData;
            gunAnimatorController.ReloadAnimator(gunData.gunAnimator);
            CurState = GunState.Idle;
        }

        public void GunShot(Vector3 target,Vector3 shotCenter,Vector2 dir)
        {
            var shotPoint = shotCenter.GetDirDistance(target, gunData.shotLength);
            var smokePoint = shotCenter.GetDirDistance(target, gunData.shotLength-0.2f);
            
            FxPlayer.PlayFx(gunData.shotFx, shotPoint).Rotate(GetAngle.Angle(dir));
            FxPlayer.PlayFx(gunData.smokeFx, smokePoint).Rotate(GetAngle.Angle(dir));
            CameraShake.Instance.ShakeCamera(0.1f,0.1f);
            CurState = GunState.Shot;
        }
        
        public void AnimatorStateEnter()
        {
            switch (CurState)
            {
                case GunState.Shot:

                    fireLight.enabled = true;
                    this.DelayExecute(0.07f, () =>
                    {
                        fireLight.enabled = false;
                    });
                    break;
            }
        }

        public void AnimatorStateComplete()
        {
            switch (CurState)
            {
                case GunState.Shot:
                    CurState = GunState.Idle;
                    break;
            }
        }
    }    
}
