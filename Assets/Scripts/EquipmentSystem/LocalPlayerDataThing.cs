using System;
using data;

namespace EquipmentSystem
{
    [Serializable]
    public class LocalPlayerDataThing
    {
        public WeaponData weapon_1;
        public WeaponData weapon_2;
        public MeleeWeaponData meleeWeapon;
        public AccessoryData accessory;
        public SpellData spell;
        public PackageData package;
        public CapData capData;
        public CoatData coatData;
        public ShoeData leftShoe;
        public ShoeData rightShoe;
        public ShieldData shieldData;
        public Consumable_Data consumable_1;
        public Consumable_Data consumable_2;
        public Consumable_Data consumable_3;
        public Consumable_Data consumable_4;
        
        // public List<PackageItemData> localPackageDataList;
        
        public LocalPlayerDataThing()
        {
            weapon_1 = new WeaponData("");
            weapon_2 = new WeaponData("");
            meleeWeapon = new MeleeWeaponData("");
            accessory = new AccessoryData("");
            spell = new SpellData("");
            package = new PackageData("");
            
            capData = new CapData("");
            coatData = new CoatData("");
            leftShoe = new ShoeData("");
            rightShoe = new ShoeData("");
            shieldData = new ShieldData("");
            consumable_1 = new Consumable_Data("");
            consumable_2 = new Consumable_Data("");
            consumable_3 = new Consumable_Data("");
            consumable_4 = new Consumable_Data("");
        }


        public static KeepDataHandler<LocalPlayerDataThing> GetDataHandler()
        {
            return DataManager.Instance.LocalPlayerDataThingHandler;
        }
        
        public static LocalPlayerDataThing GetData()
        {
            return DataManager.Instance.LocalPlayerDataThingHandler.Data;
        }

        public static void Save()
        {
            GetDataHandler().SaveData();
        }
    }    
}
