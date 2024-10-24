using System.Collections;
using System.Collections.Generic;
using buff;
using data;
using DG.Tweening;
using game;
using GridSystem;
using item;
using other;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EquipmentSystem
{
    public class BaseGun:BaseEquipment
    {
        public GunData gunData{ get; set; }

        public GunParameter gunParameter { get; set; }
        public float shotDelay => playerController.playerEquipment.shotDelay;
        public float bulletSpeed => playerController.playerEquipment.bulletSpeed;
        public float reloadTime => playerController.playerEquipment.reloadTime;
        
        public float maxShotShake => playerController.playerEquipment.maxShotShake;
        public float minShotShake => playerController.playerEquipment.minShotShake;

        public float aimingShotShake => playerController.PlayerHand.aimingShake;
        public float shotStability => playerController.playerEquipment.shotStability;
        public float shotCalibration => playerController.playerEquipment.shotCalibration;
        public int maxAmmo => playerController.playerEquipment.maxAmmo;

        public int Damage => gunParameter.Damage;
        
        public float currentShotShake{ get; set; }
        // public float basicShotShake{ get; set; }
        public float range => currentShotShake/2f  + minShotShake - aimingShotShake;

        public WeaponData WeaponData { get; private set; }
        // protected GameObject GunObj;
        protected Transform shotCenter;
        protected PlayerController playerController;
        protected InputActionPhase curPhase;
        protected bool shotLock = false;
        protected bool isReloading = false;
        private float calibrateTime = 0.1f;

        // private GunAnimatorController gunAnimatorController;
        protected GunObject gunObject;
    
        // private int _currentAmmo;
        public int currentAmmo
        {
            get => WeaponData.currentAmmo;
            protected set
            {
                WeaponData.currentAmmo = value;
                PlayerUiPanel.Instance.UpdateGunBar(WeaponData.currentAmmo,maxAmmo);
            }
        }
        private bool CanShot
        {
            get
            {
                if (isReloading || currentAmmo<=0 || shotLock)
                    return false;
            
                shotLock = true;
                playerController.DelayExecute(shotDelay, () => shotLock = false);
                return true;
            }
        }

        public BaseGun(PlayerController playerController,GunObject gunObj,Transform shotCenter,GunData gunData,WeaponData weaponData):base(playerController)
        {
            this.WeaponData = weaponData;
            gunObject = gunObj;
            this.shotCenter = shotCenter;
            this.playerController = playerController;
            this.gunData = gunData;
            
            gunObject.ReloadGun(gunData);
            gunParameter = new GunParameter(this.gunData);
            // currentShotShake = basicShotShake;
        }

        public void GunFire(InputAction.CallbackContext context)
        {
            curPhase = context.phase;
            if(context.phase == InputActionPhase.Started && gunParameter.shotMode == ShotMode.Click)
                CheckFire();
            if (context.phase == InputActionPhase.Performed && gunParameter.shotMode == ShotMode.Press)
            {
                playerController.WaitExecute(()=>curPhase == InputActionPhase.Canceled,null,CheckFire);
            }
        }
        
        public void ReloadBullet(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Started)
                BulletReLoad();
        }
        
        public void CheckFire()
        {
            if (CanShot)
                Shot(gunData.bulletData);
            else
            {
                // Debug.Log("CATA!");
            }
        }

        protected void BulletReLoad()
        {
            isReloading = true;
            PlayerUiPanel.Instance.GunReloading(reloadTime);
            playerController.DelayExecute(reloadTime, () =>
            {
                int packageBullet = playerController.playerEquipment.package.GetBulletFormPackage(gunData.bulletData.Name,maxAmmo - currentAmmo);
                currentAmmo = currentAmmo + packageBullet;
                isReloading = false;
                DataManager.Instance.SaveAllData();
            });
        }
        public virtual void Shot(BulletData bulletData) 
        {
            var dir = (GameCursor.Instance.transform.position - shotCenter.position).normalized;
            var shotPoint = shotCenter.position.GetDirDistance(GameCursor.Instance.transform.position, gunData.shotLength);

            var afterDir = dir.Rota2DAxis(Random.Range(-range, range));
            Bullet baseBullet = Load<Bullet>(bulletData);

            gunObject.GunShot(shotCenter.position,afterDir);
            baseBullet.BulletPrepare(shotPoint,afterDir,this);
            
            
            currentShotShake += shotStability;
            if(currentShotShake > maxShotShake)
                currentShotShake = maxShotShake;
            
            if(currentShotShake > 0)
                GunCalibration();
            currentAmmo--;
        }
        
        protected void GunCalibration()
        {
            float eachCalibration = shotCalibration / (calibrateTime/0.02f);
            playerController.WaitFixedExecute(()=> currentShotShake <= 0, () =>
            {
                currentShotShake = 0;
            }, () =>
            {
                currentShotShake -= eachCalibration;
            });
        }
        
        public static T Load<T>(BulletData data) where T:Bullet
        {
            string path = $"Prefab/Bullet/{data.Name}";
            return PoolManager.Instance.PopObj<T>(data.Name,path);
        }

       
    }
    
}
