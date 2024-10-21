using System.Collections;
using System.Collections.Generic;
using data;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class Consumable : BaseEquipment
    {
        private int index;
        Consumable_Data consumableData;
        
        public Consumable(PlayerController playerController,int index) : base(playerController)
        {
            this.index = index;
            CheckConsumable();
        }
        public override void UpdateEquipment()
        {
            CheckConsumable();
        }
        
        public void CheckConsumable()
        {
            switch (index)
            {
                case 1:
                    consumableData = LocalPlayerDataThing.GetData().consumable_1;
                    break;
                case 2:
                    consumableData = LocalPlayerDataThing.GetData().consumable_2;
                    break;
                case 3:
                    consumableData = LocalPlayerDataThing.GetData().consumable_3;
                    break;
                case 4:
                    consumableData = LocalPlayerDataThing.GetData().consumable_4;
                    break;        
            }
        }
    }
    
}
