using System.Collections;
using System.Collections.Generic;
using data;
using EquipmentSystem;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class MeleeWeapon : BaseWeapon
    {
        private MeleeWeaponData weaponData;
        
        public MeleeWeapon(PlayerController playerController) : base(playerController)
        {
            weaponData = LocalPlayerDataThing.GetData().meleeWeapon;
        }
        public override void UpdateEquipment()
        {
            weaponData = LocalPlayerDataThing.GetData().meleeWeapon;
        }

        
    }
    
}
