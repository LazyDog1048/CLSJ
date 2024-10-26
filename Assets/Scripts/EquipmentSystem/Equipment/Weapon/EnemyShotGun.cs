using data;
using game;
using other;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class EnemyShotGun : EnemyGun
    {
        [SerializeField]
        private float delay = 0.12f;
        [SerializeField]
        private int bulletNum = 5;
        
        public EnemyShotGun(MonoBehaviour mono,GunObject gunObj,Transform shotCenter,GunData gunData):base(mono,gunObj,shotCenter,gunData)
        {
        }
        
        public override void Shot(Vector3 target) 
        {
            var curRange = range;
            // var dir = (GameCursor.Instance.transform.position - shotCenter.position).normalized;
            var shotPoint = shotCenter.position.GetDirDistance(target, 1);
            var dir = (target - shotCenter.position).normalized;
            
            GunObject.GunShot(target,shotCenter.position,dir);
            for (int i = 0; i < bulletNum; i++)
            {
                mono.DelayExecute(Random.Range(0,delay), () =>
                {
                    var afterDir = dir.Rota2DAxis(Random.Range(-curRange, curRange));
                    Bullet baseBullet = Load<Bullet>(gunData.bulletData);
                    baseBullet.BulletPrepare(shotPoint,afterDir,gunParameter);
                });
            }
            
            //开始偏移
            if (currentShotShake < 0.01f)
                GunCalibration();
            
            currentShotShake += shotStability;
            if(currentShotShake > maxShotShake)
                currentShotShake = maxShotShake;
            
            // currentAmmo--;
        }
    }
    
}
