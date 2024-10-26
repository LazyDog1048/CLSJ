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
    public class EnemyGun:AbstractComponent
    {
        public GunData gunData{ get; set; }

        public GunParameter gunParameter { get; set; }
        public virtual float shotDelay => gunParameter.shotDelay.FinalValue;
        public virtual float bulletSpeed => gunParameter.bulletSpeed.FinalValue;
        public virtual float reloadTime => gunParameter.reloadTime.FinalValue;
        
        public virtual float maxShotShake => gunParameter.maxShotShake.FinalValue;
        public virtual float minShotShake => gunParameter.minShotShake.FinalValue;

        public virtual float aimingShotShake => gunParameter.minCalibration;
        public virtual float shotStability => gunParameter.shotStability.FinalValue;
        public virtual float shotCalibration => gunParameter.shotCalibration.FinalValue;
        public virtual int maxAmmo => gunParameter.maxAmmo.FinalValue;

        public virtual int Damage => gunParameter.Damage;
        
        public virtual float currentShotShake{ get; set; }
        public virtual float range => currentShotShake/2f  + minShotShake - aimingShotShake;

        protected Transform shotCenter;

        protected bool shotLock = false;
        protected bool isReloading = false;
        private float calibrateTime = 0.1f;

        
        protected GunObject GunObject;

        public int currentAmmo { get; protected set; }

        protected virtual bool CanShot
        {
            get
            {
                if (isReloading || currentAmmo<=0)
                    return false;
                return true;
            }
        }

        public EnemyGun(MonoBehaviour mono,GunObject gunObj,Transform shotCenter,GunData gunData):base(mono)
        {
            
            GunObject = gunObj;
            this.shotCenter = shotCenter;
            this.gunData = gunData;
            gunParameter = new GunParameter(this.gunData);
            currentAmmo = maxAmmo;
        }

        

        public void CheckFire(Vector3 target)
        {
            if (CanShot)
                Shot(target);
            else
            {
                // Debug.Log("CATA!");
            }
        }

        protected virtual void BulletReLoad()
        {
            isReloading = true;
            mono.DelayExecute(reloadTime, () =>
            {
                currentAmmo = maxAmmo;
                isReloading = false;
            });
        }
        
        public virtual void Shot(Vector3 target) 
        {
            var dir = (target - shotCenter.position).normalized;
            var shotPoint = shotCenter.position.GetDirDistance(target, gunData.shotLength);

            var afterDir = dir.Rota2DAxis(Random.Range(-range, range));
            Bullet baseBullet = Load<Bullet>(gunData.bulletData);

            GunObject.GunShot(target,shotCenter.position,afterDir);
            baseBullet.BulletPrepare(shotPoint,afterDir,gunParameter);
            
            
            currentShotShake += shotStability;
            if(currentShotShake > maxShotShake)
                currentShotShake = maxShotShake;
            
            if(currentShotShake > 0)
                GunCalibration();
            // currentAmmo--;
            
            if(currentAmmo <= 0)
                BulletReLoad();
        }
        
        protected void GunCalibration()
        {
            float eachCalibration = shotCalibration / (calibrateTime/0.02f);
            mono.WaitFixedExecute(()=> currentShotShake <= 0, () =>
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
