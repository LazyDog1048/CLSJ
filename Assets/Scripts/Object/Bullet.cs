using System;
using buff;
using tool;
using UnityEngine;
using UnityEngine.Events;

namespace game
{
    public class Bullet : MonoPoolObj
    {
        #region get&set
        [SerializeField]
        private BulletData data;

        private Gun gun;
        // private BulletParameter parameter { get; set; }
        


        #region paramater
        
        // protected EventExtraFloat moveSpeed = new EventExtraFloat(0);


        protected Vector2 currentDir;
        
        #endregion
        
        #endregion
        protected override void OnAwake()
        {
            base.OnAwake();
            
            // parameter = new BulletParameter(data);
            // moveSpeed = new EventExtraFloat(parameter.speed);
            
        }

        #region BulletAction
        
        public virtual void BulletPrepare(Vector3 shot,Vector2 dir,Gun gun)
        {
            this.gun = gun;
            transform.position = shot;
            currentDir = dir;
            float angle = GetAngle.Angle(currentDir);
            transform.localRotation = Quaternion.Euler(0,0,angle);
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
            transform.position += (Vector3)currentDir * gun.bulletSpeed.FinalValue * Time.deltaTime;
        }
        
        protected virtual void FinishBullet()
        {
            HitPoint();
            ReleaseObj();
        }

        #endregion

        #region Hit

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log($"hit {col.name}");
            FinishBullet();
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            
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
      
        public override string poolId => data.name;
        
        public override void OnPushObj()
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            base.OnPushObj();
        }

        #endregion

        
   
    }
    
}
