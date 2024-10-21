using System.Collections;
using System.Collections.Generic;
using data;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class Armor_Cap : BaseEquipment
    {
        public int reloadRate => capData.reloadRate;
        public int shotCalibration => capData.shotCalibration;
        public int shotStability => capData.shotStability;
        
        private CapData capData;
        
        public Armor_Cap(PlayerController playerController) : base(playerController)
        {
            capData = LocalPlayerDataThing.GetData().capData;
        }
        
        public override void UpdateEquipment()
        {
            capData = LocalPlayerDataThing.GetData().capData;
        }
    }
    
}
