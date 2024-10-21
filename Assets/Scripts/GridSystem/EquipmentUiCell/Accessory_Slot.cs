using data;
using EquipmentSystem;

namespace GridSystem
{ 
    public class Accessory_Slot : PlayerEquipmentSlot
    {
        public override PackageItemType ItemType => PackageItemType.Accessory;
        public  AccessoryData accessoryData => packageItemData as AccessoryData;

        protected override void Init()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            packageItemData = localPlayerDataThing.accessory;
            base.Init();
        }

        public override void SavePlayerEquipmentSlotData()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            localPlayerDataThing.accessory = accessoryData;
            LocalPlayerDataThing.Save();
        }
    }
}
