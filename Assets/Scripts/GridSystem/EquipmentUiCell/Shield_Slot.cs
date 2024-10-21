using data;
using EquipmentSystem;

namespace GridSystem
{
    public class Shield_Slot : PlayerEquipmentSlot
    {
        ShieldData shieldData => packageItemData as ShieldData;
        public override PackageItemType ItemType => PackageItemType.Shield;

        protected override void Init()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            packageItemData = localPlayerDataThing.shieldData;
            base.Init();
        }

        public override void SavePlayerEquipmentSlotData()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            localPlayerDataThing.shieldData = shieldData;
            LocalPlayerDataThing.Save();
        }
    }
    
}
