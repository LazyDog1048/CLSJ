using data;
using EquipmentSystem;

namespace GridSystem
{
    public class MeleeWeaponSlot : PlayerEquipmentSlot
    {
        public override PackageItemType ItemType => PackageItemType.MeleeWeapon;

        public MeleeWeaponData meleeWeaponData => packageItemData as MeleeWeaponData;
        
        protected override void Init()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            packageItemData = localPlayerDataThing.meleeWeapon;
            base.Init();
        }
        
        public override void SavePlayerEquipmentSlotData()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            localPlayerDataThing.meleeWeapon = meleeWeaponData;
            LocalPlayerDataThing.Save();
        }
    }
    
} 

