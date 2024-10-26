using System.Collections;
using System.Collections.Generic;
using data;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class BaseEquipment
    {
        protected MonoBehaviour playerController;
        
        
        public BaseEquipment(PlayerController playerController)
        {
            this.playerController = playerController;
        }
        
        
        
        public virtual void UpdateEquipment()
        {
            
        }
    }
    
}
