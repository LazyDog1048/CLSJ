using System;
using System.Collections.Generic;
using data;
using GridSystem;
using UnityEngine;

namespace EquipmentSystem
{
    [Serializable]
    public class LocalPackageThing
    {
        public List<WeaponData> weaponDataList;
        public List<MeleeWeaponData> meleeWeaponDataList;
        public List<CapData> capDataList;
        public List<CoatData> coatDataList;
        public List<ShoeData> shoeDataList;
        public List<SpellData> spellDataList;
        public List<ShieldData> shieldDataList;
        public List<AccessoryData> accessoryDataList;
        public List<Consumable_Data> consumableDataList;
        public List<Bullet_Data> bulletDataList;
        public List<PackageData> packageDataList;
        
        
        public LocalPackageThing()
        {
            weaponDataList = new List<WeaponData>();
            meleeWeaponDataList = new List<MeleeWeaponData>();
            capDataList = new List<CapData>();
            coatDataList = new List<CoatData>();
            shoeDataList = new List<ShoeData>();
            spellDataList = new List<SpellData>();
            shieldDataList = new List<ShieldData>();
            accessoryDataList = new List<AccessoryData>();
            consumableDataList = new List<Consumable_Data>();
            bulletDataList = new List<Bullet_Data>();
            packageDataList = new List<PackageData>();
        }

        public void LoadPlayerPackage(UiPackageItem uiItemOri,PlayerPackageUiGridSystem playerPackageUiGridSystem)
        {
            foreach (var item in weaponDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
            foreach (var item in meleeWeaponDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
            foreach (var item in capDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
            foreach (var item in coatDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
            foreach (var item in shoeDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
            foreach (var item in spellDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
            foreach (var item in shieldDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
            foreach (var item in accessoryDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
            foreach (var item in consumableDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
            foreach (var item in bulletDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
            foreach (var item in packageDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
        }
        

        public void AddPackageData(UiPackageItem uiPackageItem)
        {
            switch (uiPackageItem.packageItemSoData.ItemType)
            {
                case PackageItemType.Weapon:
                    weaponDataList.Add(uiPackageItem.packageItemData as WeaponData);
                    break;
                case PackageItemType.MeleeWeapon:
                    meleeWeaponDataList.Add(uiPackageItem.packageItemData as MeleeWeaponData);
                    break;
                case PackageItemType.Armor_Cap:
                    capDataList.Add(uiPackageItem.packageItemData as CapData);
                    break;
                case PackageItemType.Armor_Coat:
                    coatDataList.Add(uiPackageItem.packageItemData as CoatData);
                    break;
                case PackageItemType.Armor_LeftShoe:
                    shoeDataList.Add(uiPackageItem.packageItemData as ShoeData);
                    break;
                case PackageItemType.Armor_RightShoe:
                    shoeDataList.Add(uiPackageItem.packageItemData as ShoeData);
                    break;
                case PackageItemType.Spell:
                    spellDataList.Add(uiPackageItem.packageItemData as SpellData);
                    break;
                case PackageItemType.Shield:
                    shieldDataList.Add(uiPackageItem.packageItemData as ShieldData);
                    break;
                case PackageItemType.Accessory:
                    accessoryDataList.Add(uiPackageItem.packageItemData as AccessoryData);
                    break;
                case PackageItemType.Consumable:
                    consumableDataList.Add(uiPackageItem.packageItemData as Consumable_Data);
                    break;
                case PackageItemType.Bullet:
                    bulletDataList.Add(uiPackageItem.packageItemData as Bullet_Data);
                    break;
                case PackageItemType.Package:
                    packageDataList.Add(uiPackageItem.packageItemData as PackageData);
                    break;
                default:
                    break;
            }
            
        }
        
        public void ClearPackageData()
        {
            weaponDataList.Clear();
            meleeWeaponDataList.Clear();
            capDataList.Clear();
            coatDataList.Clear();
            shoeDataList.Clear();
            spellDataList.Clear();
            shieldDataList.Clear();
            accessoryDataList.Clear();
            consumableDataList.Clear();
            bulletDataList.Clear();
            packageDataList.Clear();
            // localPackageDataList.Clear();
        }
        
       
        
        public static KeepDataHandler<LocalPackageThing> GetDataHandler()
        {
            return DataManager.Instance.LocalPackageThingHandler;
        }
        
        public static LocalPackageThing GetData()
        {
            return DataManager.Instance.LocalPackageThingHandler.Data;
        }

        public static void Save()
        {
            GetDataHandler().SaveData();
        }
    }    
}
