using data;
using EquipmentSystem;

namespace GridSystem
{
    public class Spell_Slot : PlayerEquipmentSlot
    {
        public override PackageItemType ItemType => PackageItemType.Spell;
        public SpellData spellData => packageItemData as SpellData;

        protected override void Init()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            packageItemData = localPlayerDataThing.spell;
            base.Init();
        }

        public override void SavePlayerEquipmentSlotData()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            localPlayerDataThing.spell =  spellData;
            LocalPlayerDataThing.Save();
        }
    }
    
}
