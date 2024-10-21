using data;
using EquipmentSystem;

namespace GridSystem
{
    public class Package_Slot : PlayerEquipmentSlot
    {
        public override PackageItemType ItemType => PackageItemType.Package;
        public PackageData packageData => packageItemData as PackageData;
        
        protected override void Init()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            packageItemData = localPlayerDataThing.package;
            base.Init();
        }
        
        public override void SavePlayerEquipmentSlotData()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            localPlayerDataThing.package = packageData;
            LocalPlayerDataThing.Save();
        }
    }
    
} 

