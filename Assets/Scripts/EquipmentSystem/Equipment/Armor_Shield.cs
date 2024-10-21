using System.Collections;
using System.Collections.Generic;
using data;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class Armor_Shield : BaseEquipment
    {
        private ShieldData shieldData;
        
        public Armor_Shield(PlayerController playerController) : base(playerController)
        {
            shieldData = LocalPlayerDataThing.GetData().shieldData;
        }
        
        public override void UpdateEquipment()
        {
            shieldData = LocalPlayerDataThing.GetData().shieldData;
        }
    }
    
}
