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
        private Light2D fireLight;
        private GunData gunData;
        public GunState CurState
        {
            get => gunAnimatorController.CurState;
            set => gunAnimatorController.SetAnim(value);
        }
        private GunAnimatorController gunAnimatorController;
        private SpriteRenderer icon;

        public void Init()
        {
            gunAnimatorController = new GunAnimatorController(this);
        }

        public void ReloadGun(GunData data)
        {
            gunData = data;
            gridObjectSo = data;
            gunAnimatorController.ReloadAnimator(data.gunAnimator);
            CurState = GunState.Idle;
        }

        public void GunShot(Vector3 shotCenter,Vector2 dir)
        {
            var shotPoint = shotCenter.GetDirDistance(GameCursor.Instance.transform.position, gunData.shotLength);
            var smokePoint = shotCenter.GetDirDistance(GameCursor.Instance.transform.position, gunData.shotLength-0.2f);
            
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
