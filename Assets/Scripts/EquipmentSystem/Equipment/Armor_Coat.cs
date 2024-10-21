using System.Collections;
using System.Collections.Generic;
using data;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class Armor_Coat : BaseEquipment
    {
        public int Hp=>coatData.maxHp;
        public int PDef=>coatData.pDef;
        
        private CoatData coatData;
        
        public Armor_Coat(PlayerController playerController) : base(playerController)
        {
            coatData = LocalPlayerDataThing.GetData().coatData;
        }
        
        public override void UpdateEquipment()
        {
            coatData = LocalPlayerDataThing.GetData().coatData;
        }

        
    }
    
}
