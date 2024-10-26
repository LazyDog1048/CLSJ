using data;
using EquipmentSystem;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EquipmentSystem
{
    public class Hand : PlayerGun
    {

        public Hand(PlayerController playerController, PlayerGunObject playerGunObj, Transform shotCenter, GunData gunData, WeaponData weaponData) : base(playerController, playerGunObj, shotCenter, gunData, weaponData)
        {
        }
        
        public override void GunFire(InputAction.CallbackContext context)
        {
            
        }

        protected override void BulletReLoad()
        {
            
        }

        public override void Shot(BulletData bulletData)
        {
            
        }
    }
    
}
