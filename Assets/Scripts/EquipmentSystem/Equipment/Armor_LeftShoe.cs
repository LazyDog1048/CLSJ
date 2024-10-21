using System.Collections;
using System.Collections.Generic;
using data;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class Armor_LeftShoe : BaseEquipment
    {
        public int Hp=>shoeData.maxHp;
        public int PDef=>shoeData.pDef;
        public int SpeedRate=>shoeData.speedRate;
        
        private ShoeData shoeData;

        public Armor_LeftShoe(PlayerController playerController) : base(playerController)
        {
            shoeData = LocalPlayerDataThing.GetData().leftShoe;
        }
        
        public override void UpdateEquipment()
        {
            shoeData = LocalPlayerDataThing.GetData().leftShoe;
        }

       
    }
    
}
