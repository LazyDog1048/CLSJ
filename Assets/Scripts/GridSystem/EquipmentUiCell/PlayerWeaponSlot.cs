using data;
using EquipmentSystem;
using UnityEngine;

namespace GridSystem
{
    public class PlayerWeaponSlot : PlayerEquipmentSlot
    {
        public bool first = true;
        public override PackageItemType ItemType => PackageItemType.Weapon;
        protected WeaponData weaponData=>packageItemData as WeaponData;
        protected override void Init()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            packageItemData = first ? localPlayerDataThing.weapon_1 : localPlayerDataThing.weapon_2;
            base.Init();
        }
        
        public override void SavePlayerEquipmentSlotData()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            if(first)
                localPlayerDataThing.weapon_1 = weaponData;
            else
                localPlayerDataThing.weapon_2 = weaponData;
            LocalPlayerDataThing.Save();
        }
    }
    
}
