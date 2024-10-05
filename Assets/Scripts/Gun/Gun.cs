using System;
using System.Collections;
using System.Collections.Generic;
using buff;
using other;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace game
{
    public class Gun : MonoBehaviour
    {
        
        public GunData gunData;

        public GunParameter gunParameter { get; set; }

        public EventExtraFloat shotDelay{ get; set; }
        public EventExtraFloat bulletSpeed{ get; set; }
        public EventExtraFloat reloadTime{ get; set; }
        
        public float shotShake{ get; set; }
        
        public float basicShotShake{ get; set; }
        public EventExtraFloat maxShotShake{ get; set; }
        public EventExtraFloat minShotShake{ get; set; }
        public EventExtraFloat shotStability{ get; set; }
        
        public EventExtraInt maxAmmo{ get; set; }
        public float range => shotShake/2f + basicShotShake + minShotShake.FinalValue;
        protected Transform shotPoint;

        protected InputActionPhase curPhase;
        protected bool shotLock = false;

        private float calibrateTime = 0.1f;
        private int _currentBullet;
        protected int currentBullet
        {
            get => _currentBullet;
            set
            {
                _currentBullet = value;
                PlayerUi.Instance.UpdateGunBar(_currentBullet,maxAmmo.FinalValue);
            }
        }
        private bool CanShot
        {
            get
            {
                if (currentBullet<=0 || shotLock)
                    return false;
            
                shotLock = true;
                this.DelayExecute(shotDelay.FinalValue, () => shotLock = false);
                return true;
            }
        }
        private void Awake()
        {
            shotPoint = transform.Find("ShotPoint");
            gunParameter = new GunParameter(gunData);
            
            shotDelay = new EventExtraFloat(gunParameter.shotDelay);
            maxShotShake = new EventExtraFloat(gunParameter.maxShotShake);
            minShotShake = new EventExtraFloat(gunParameter.minShotShake);
            shotStability = new EventExtraFloat(gunParameter.shotStability);
            
            bulletSpeed = new EventExtraFloat(gunParameter.bulletSpeed);
            reloadTime = new EventExtraFloat(gunParameter.reloadTime);
            maxAmmo = new EventExtraInt(gunParameter.maxAmmo);
            
            shotShake = basicShotShake;
        }

        private void Start()
        {
            currentBullet = maxAmmo.FinalValue;
        }

        public void GunFire(InputAction.CallbackContext context)
        {
            curPhase = context.phase;
            if(context.phase == InputActionPhase.Started && gunParameter.shotMode == ShotMode.Click)
                CheckFire();
            if (context.phase == InputActionPhase.Performed && gunParameter.shotMode == ShotMode.Press)
            {
                this.WaitExecute(()=>curPhase == InputActionPhase.Canceled,null,CheckFire);
            }
        }
        public void CheckFire()
        {
            if (CanShot)
                Shot(gunData.bulletData);
        }

        protected void BulletReLoad()
        {
            this.DelayExecute(reloadTime.FinalValue, () => currentBullet = maxAmmo.FinalValue);
        }
        public virtual void Shot(BulletData bulletData) 
        {
            var dir = (GameCursor.Instance.transform.position - shotPoint.position).normalized;
            var afterDir = dir.Rota2DAxis(Random.Range(-range, range));
            Bullet baseBullet = Load<Bullet>(bulletData);
            baseBullet.BulletPrepare(shotPoint.position,afterDir,this);
            
            //开始偏移
            if (shotShake < 0.01f)
                GunCalibration();
            
            shotShake += shotStability.FinalValue;
            if(shotShake > maxShotShake.FinalValue)
                shotShake = maxShotShake.FinalValue;


            currentBullet--;
            if(currentBullet <=0)
                BulletReLoad();
        }
        
        protected void GunCalibration()
        {
            this.LoopDelayExecute(calibrateTime,()=> shotShake <= 0, () =>
            {
                shotShake -= gunParameter.shotCalibration;
                if(shotShake < 0)
                    shotShake = 0;
            });
        }
        
        public static T Load<T>(BulletData data) where T:Bullet
        {
            string path = $"Prefab/Bullet/{data.name}";
            return PoolManager.Instance.PopObj<T>(data.name,path);
        }
        
    }
    
}
