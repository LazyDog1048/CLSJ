using data;
using EquipmentSystem;

namespace GridSystem
{
    public class Armor_RightShoe_Slot : PlayerEquipmentSlot
    {
        private ShoeData rightShoeData => packageItemData as ShoeData;
        public override PackageItemType ItemType => PackageItemType.Armor_RightShoe;
        
        protected override void Init()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            packageItemData = localPlayerDataThing.rightShoe;
            base.Init();
        }
        public override void SavePlayerEquipmentSlotData()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            localPlayerDataThing.rightShoe = rightShoeData;
            LocalPlayerDataThing.Save();
        }
    }
    
} 
