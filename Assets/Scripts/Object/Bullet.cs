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

        private BulletParameter parameter { get; set; }
        


        #region paramater
        
        protected EventExtraFloat moveSpeed = new EventExtraFloat(0);


        protected Vector2 currentDir;
        
        #endregion
        
        #endregion
        protected override void OnAwake()
        {
            base.OnAwake();
            
            parameter = new BulletParameter(data);
            moveSpeed = new EventExtraFloat(parameter.speed);
            
        }

        #region BulletAction
        
        public virtual void BulletPrepare(Vector3 shot,Vector3 target)
        {
            currentDir = (target - shot).normalized;
            transform.position = shot;
            float angle = GetAngle.Angle(shot,target);
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
            this.DelayExecute(parameter.stayTime, ReleaseObj);
        }

        private void Update()
        {
            UpdateBullet();
        }

        protected virtual void UpdateBullet()
        {
            transform.position += (Vector3)currentDir * moveSpeed.FinalValue * Time.deltaTime;
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
      
        public override string poolId => parameter.name;
        
        public override void OnPushObj()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            base.OnPushObj();
        }

        #endregion

        
   
    }
    
}
