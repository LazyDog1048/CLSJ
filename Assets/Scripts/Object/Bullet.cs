using System;
using EquipmentSystem;
using game;
using GridSystem;
using item;
using UnityEngine;

namespace EquipmentSystem
{
    public class Bullet : GridGameObject
    {
        #region get&set

        private int penetrateNum = 1;
        public BaseGun gun { get;private set; }

        private TrailRenderer trail;

        private float angle;
        #region paramater


        public Vector2 currentDir { get; private set; }
        private Vector3 startPos;
        #endregion
        
        #endregion

        #region BulletAction

        protected override void OnAwake()
        {
            base.OnAwake();
            trail = GetComponentInChildren<TrailRenderer>();
        }

        public virtual void BulletPrepare(Vector3 shot,Vector2 dir,BaseGun gun)
        {
            this.gun = gun;
            transform.position = shot;
            startPos = shot;
            currentDir = dir;
            angle = GetAngle.Angle(currentDir);
            transform.localRotation = Quaternion.Euler(0,0,angle);
            penetrateNum = gun.gunParameter.penetrateNum;
            ResetTrailEffect();
            BulletShot();
        }

        private void BulletShot()
        {
            ShotFxPlay();
            BulletDelay();
        }

        private void BulletDelay()
        {
            this.DelayExecute(gun.gunParameter.bulletStayTime, ReleaseObj);
        }

        private void Update()
        {
            UpdateBullet();
        }

        protected virtual void UpdateBullet()
        {
            transform.position += (Vector3)currentDir * gun.bulletSpeed * Time.deltaTime;
        }
        
        protected virtual void FinishBullet()
        {
            HitPoint();
            ReleaseObj();
        }

        #endregion

        #region Hit

        // private void OnCollisionEnter2D(Collision2D col)
        // {
        //     Debug.Log($"hit {col.collider.name}");
        //     penetrateNum--;
        //     if (col.collider.tag.Equals("Scene"))
        //     {
        //         Vector3 point = col.GetContact(0).point;
        //         FxPlayer.PlayFx("Fx_Gun_Miss", point).Rotate(angle);
        //     }
        //     if(penetrateNum <= 0)
        //         FinishBullet();
        //     
        // }

        
        private void OnTriggerEnter2D(Collider2D col)
        {
            penetrateNum--;
            if (col.tag.Equals("Scene"))
            {
                var ray = Physics2D.Raycast(startPos,currentDir,100, LayerMask.GetMask("Wall"));
                if (ray.collider == col)
                {
                    FxPlayer.PlayFx("Fx_Gun_Miss", ray.point).Rotate(angle);
                    // FinishBullet();
                }
            }
            else if (col.tag.Equals("HitObj"))
            {
                var hit = col.transform.GetComponentInParent<IHitObj>();
                hit?.HitObj(this);
            }
            if(penetrateNum <= 0)
                FinishBullet();
        }

   
        protected virtual void HitPoint()
        {
            HitFxPlay();
        }

        protected virtual void ShotFxPlay()
        {
            
        }

        protected virtual void HitFxPlay()
        {
        }
      
        public override string poolId => gridObjectSo.Name;
        
        public override void OnPushObj()
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            ResetTrailEffect();
            base.OnPushObj();
        }

        void ResetTrailEffect()
        {
            if(trail == null)
                return;
            trail.enabled = true;
            trail.Clear();  // 清除当前的拖尾
        }

        #endregion

        
   
    }
    
}
