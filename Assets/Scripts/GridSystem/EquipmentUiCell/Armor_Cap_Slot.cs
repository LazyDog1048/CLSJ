using data;
using EquipmentSystem;

namespace GridSystem
{
    public class Armor_Cap_Slot : PlayerEquipmentSlot
    {
        public override PackageItemType ItemType => PackageItemType.Armor_Cap;
        private CapData capData => packageItemData as CapData;


        protected override void Init()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            packageItemData = localPlayerDataThing.capData;
            base.Init();
        }

        public override void SavePlayerEquipmentSlotData()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            localPlayerDataThing.capData = capData;
            LocalPlayerDataThing.Save();
        }
    }
}