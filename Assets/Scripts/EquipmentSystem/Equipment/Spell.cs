using System.Collections;
using System.Collections.Generic;
using data;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class Spell : BaseEquipment
    {
        public SpellData spellData;
        
        public Spell(PlayerController playerController) : base(playerController)
        {
            spellData = LocalPlayerDataThing.GetData().spell;
        }
        public override void UpdateEquipment()
        {
            spellData = LocalPlayerDataThing.GetData().spell;
        }

        
    }
    
}
