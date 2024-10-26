using data;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using EquipmentSystem;
using game;
using other;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EquipmentSystem
{
    public class Lamp : PlayerGun
    {
        TweenerCore<float,float,FloatOptions> batteryTween;
        public float time { get; private set; }
        private float _currentBattery;

        public float currentBattery
        {
            get => _currentBattery;
            set => _currentBattery = value;
        }
        
        protected override bool CanShot => currentBattery > 0;

        public Lamp(PlayerController playerController, PlayerGunObject playerGunObj, Transform shotCenter, GunData gunData, WeaponData weaponData) : base(playerController, playerGunObj, shotCenter, gunData, weaponData)
        {
            Debug.Log($"New Lamp");
            time = gunData.bulletData.stayTime;
            ResetBattery();
        }

        public override void Shot(BulletData bulletData)
        {
            
        }

        
        public override void GunFire(InputAction.CallbackContext context)
        {
            curPhase = context.phase;
            if (context.phase == InputActionPhase.Started)
            {
                if (CanShot)
                    Shot(gunData.bulletData);
                else
                {
                    // Debug.Log("CATA!");
                }
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                batteryTween.Pause();
            }
        }
        
        public void LightOn()
        {
            if(!CanShot || batteryTween.IsPlaying())
                return;
            Debug.Log("LightOn");
            batteryTween.Play();
            PlayerUiPanel.Instance.UpdateBatteryBar(currentBattery, time);
            PlayerLamp.Instance.UseLamp(this);
        }
        
        public void LightOff()
        {
            Debug.Log("LightOff");
            batteryTween.Pause();
            PlayerLamp.Instance.Resume();
        }

        protected override void BulletReLoad()
        {
            LoadBattery();
        }
        
        private void Update()
        {
            PlayerUiPanel.Instance.UpdateBatteryBar(currentBattery, time);
        }
        private void Complete()
        {
            LightOff();
            currentBattery = 0;
            currentAmmo = 0;
        }
        
        private void LoadBattery()
        {
            isReloading = true;
            PlayerUiPanel.Instance.GunReloading(reloadTime);
            
            playerController.DelayExecute(reloadTime, () =>
            {
                int packageBullet = playerController.playerEquipment.package.GetBulletFormPackage(gunData.bulletData.Name,1);
                Debug.Log(packageBullet);
                isReloading = false;
                if (packageBullet > 0)
                {
                    ResetBattery();
                    LightOn();
                }
                
                DataManager.Instance.SaveAllData();
            });
        }
        
        private void ResetBattery()
        {
            currentBattery = time;
            batteryTween = DOTween.To(() => currentBattery, x => currentBattery = x, 0, time).SetEase(Ease.Linear);
            batteryTween.onUpdate = Update;
            batteryTween.OnComplete(Complete);
            batteryTween.Pause();
        }
    }
    
}
