using System.Collections;
using System.Collections.Generic;
using data;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class Accessory : BaseEquipment
    {
        public AccessoryData accessoryData;
        
        public Accessory(PlayerController playerController) : base(playerController)
        {
            accessoryData = LocalPlayerDataThing.GetData().accessory;
        }
        
        public override void UpdateEquipment()
        {
            accessoryData = LocalPlayerDataThing.GetData().accessory;
        }

        
    }
    
}
