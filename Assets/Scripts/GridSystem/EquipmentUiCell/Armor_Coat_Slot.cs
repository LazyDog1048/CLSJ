using data;
using EquipmentSystem;

namespace GridSystem
{
    public class Armor_Coat_Slot : PlayerEquipmentSlot
    {
        private CoatData coatData => packageItemData as CoatData;
        public override PackageItemType ItemType => PackageItemType.Armor_Coat;

        protected override void Init()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            packageItemData = localPlayerDataThing.coatData;
            base.Init();
        }

        public override void SavePlayerEquipmentSlotData()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            localPlayerDataThing.coatData = coatData;
            LocalPlayerDataThing.Save();
        }
    }
    
} 
