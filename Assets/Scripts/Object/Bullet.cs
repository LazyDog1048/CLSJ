using EquipmentSystem;
using game;
using GridSystem;
using UnityEngine;

namespace EquipmentSystem
{
    public class Bullet : GridGameObject
    {
        #region get&set

        private int penetrateNum = 1;
        private BaseGun gun;


        #region paramater


        protected Vector2 currentDir;
        
        #endregion
        
        #endregion

        #region BulletAction
        
        public virtual void BulletPrepare(Vector3 shot,Vector2 dir,BaseGun gun)
        {
            this.gun = gun;
            transform.position = shot;
            currentDir = dir;
            float angle = GetAngle.Angle(currentDir);
            transform.localRotation = Quaternion.Euler(0,0,angle);
            penetrateNum = gun.gunParameter.penetrateNum;
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

        private void OnTriggerEnter2D(Collider2D col)
        {
            // Debug.Log($"hit {col.name}");
            
            if(penetrateNum <= 0)
                FinishBullet();
            else
                penetrateNum--;
            
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
      
        public override string poolId => gridObjectSo.Name;
        
        public override void OnPushObj()
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            base.OnPushObj();
        }

        #endregion

        
   
    }
    
}
