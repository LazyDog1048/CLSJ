using data;
using game;
using other;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class ShotGun : BaseGun
    {
        [SerializeField]
        private float delay = 0.12f;
        [SerializeField]
        private int bulletNum = 5;
        
        public ShotGun(PlayerController playerController, GunObject gunObj,Transform shotCenter, GunData gunData,WeaponData weaponData) : base(playerController, gunObj,shotCenter, gunData,weaponData)
        {
        }
        
        public override void Shot(BulletData bulletData) 
        {
            var curRange = range;
            // var dir = (GameCursor.Instance.transform.position - shotCenter.position).normalized;
            var shotPoint = shotCenter.position.GetDirDistance(GameCursor.Instance.transform.position, 1);
            var dir = (GameCursor.Instance.transform.position - shotCenter.position).normalized;
            
            gunObject.GunShot(shotCenter.position,dir);
            for (int i = 0; i < bulletNum; i++)
            {
                playerController.DelayExecute(Random.Range(0,delay), () =>
                {
                    var afterDir = dir.Rota2DAxis(Random.Range(-curRange, curRange));
                    Bullet baseBullet = Load<Bullet>(bulletData);
                    baseBullet.BulletPrepare(shotPoint,afterDir,this);
                });
            }
            
            //开始偏移
            if (currentShotShake < 0.01f)
                GunCalibration();
            
            currentShotShake += shotStability;
            if(currentShotShake > maxShotShake)
                currentShotShake = maxShotShake;
            
            currentAmmo--;
        }

        
    }
    
}
