using System.Collections;
using System.Collections.Generic;
using data;
using EquipmentSystem;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class BaseWeapon : BaseEquipment
    {
        public override void UpdateEquipment()
        {
            
        }

        public BaseWeapon(PlayerController playerController) : base(playerController)
        {
        }
    }
    
}
