using data;
using game;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class TripleGun : BaseGun
    {
        public float tripleShotTime = 0.1f;
        
        public TripleGun(PlayerController playerController, Transform gunObj,Transform shotCenter, GunData gunData,WeaponData weaponData) : base(playerController, gunObj,shotCenter, gunData,weaponData)
        {
        }
        
        // public override void Shot(BulletData bulletData) 
        // {
        //     var time = tripleShotTime;
        //     float maxTime = tripleShotTime * 3;
        //     if(maxTime > shotDelay)
        //         time = shotDelay / 3;
        //     
        //     playerController.LoopDelayExecute(time,3, (index) =>
        //     {
        //         if(currentAmmo<=0)
        //             return;
        //         base.Shot(bulletData);
        //     });
        // }

        
    }
    
}
