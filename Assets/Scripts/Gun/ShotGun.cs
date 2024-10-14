using other;
using UnityEngine;

namespace game
{
    public class ShotGun : Gun
    {
        [SerializeField]
        private float delay = 0.1f;
        [SerializeField]
        private int bulletNum = 5;
        public override void Shot(BulletData bulletData) 
        {
            var curRange = range;
            var dir = (GameCursor.Instance.transform.position - shotPoint.position).normalized;
            for (int i = 0; i < bulletNum; i++)
            {
                this.DelayExecute(Random.Range(0,delay), () =>
                {
                    var afterDir = dir.Rota2DAxis(Random.Range(-curRange, curRange));
                    Bullet baseBullet = Load<Bullet>(bulletData);
                    baseBullet.BulletPrepare(shotPoint.position,afterDir,this);
                });
            }
            
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
    }
    
}
