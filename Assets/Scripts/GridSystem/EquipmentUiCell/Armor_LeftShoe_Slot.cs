using data;
using EquipmentSystem;

namespace GridSystem
{
    public class Armor_LeftShoe_Slot : PlayerEquipmentSlot
    {
        private ShoeData leftShoeData => packageItemData as ShoeData;
        public override PackageItemType ItemType => PackageItemType.Armor_LeftShoe;

        protected override void Init()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            packageItemData = localPlayerDataThing.leftShoe;
            base.Init();
        }

        public override void SavePlayerEquipmentSlotData()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            localPlayerDataThing.leftShoe = leftShoeData;
            LocalPlayerDataThing.Save();
        }
    }
    
} 
