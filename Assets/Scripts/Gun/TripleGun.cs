using System.Collections;
using System.Collections.Generic;
using other;
using UnityEngine;

namespace game
{
    public class TripleGun : Gun
    {
        public float tripleShotTime = 0.1f;
        public override void Shot(BulletData bulletData) 
        {
            var time = tripleShotTime;
            float maxTime = tripleShotTime * 3;
            if(maxTime > shotDelay.FinalValue)
                time = shotDelay.FinalValue / 3;
            
            this.LoopDelayExecute(time,3, (index) =>
            {
                if(currentBullet<=0)
                    return;
                base.Shot(bulletData);
            });
        }
    }
    
}
