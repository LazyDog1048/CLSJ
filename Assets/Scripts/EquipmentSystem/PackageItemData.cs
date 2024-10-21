using System;
using data;
using UnityEngine;

namespace EquipmentSystem
{
    [Serializable]
    public class PackageItemData
    {
        public string Name;
        public Vector2Int firstGridPoint;
        public bool isRotated;
        public int count;
        
        protected PackageItemData(string name,Vector2Int firstGridPoint,bool isRotated = false,int count =1)
        {
            this.Name = name;
            this.firstGridPoint = firstGridPoint;
            this.isRotated = isRotated;
            this.count = count;
        }

        
        public static PackageItemData CreatPackageItemData(PackageItemSoData soData)
        {
            PackageItemType type = soData.ItemType;
            switch (type)
            {
                case PackageItemType.Weapon:
                    return new WeaponData(soData);
                case PackageItemType.MeleeWeapon:
                    return new MeleeWeaponData(soData);
                case PackageItemType.Armor_Cap:
                    return new CapData(soData);
                case PackageItemType.Armor_Coat:
                    return new CoatData(soData);
                case PackageItemType.Armor_LeftShoe:
                    return new ShoeData(soData);
                case PackageItemType.Armor_RightShoe:
                    return new ShoeData(soData);
                case PackageItemType.Spell:
                    return new SpellData(soData);
                case PackageItemType.Shield:
                    return new ShieldData(soData);
                case PackageItemType.Accessory:
                    return new AccessoryData(soData);
                case PackageItemType.Consumable:
                    return new Consumable_Data(soData);
                case PackageItemType.Package:
                    return new PackageData(soData);
                default:
                    return new PackageItemData(soData.Name,new Vector2Int(-1,-1));
            }
        }
    }

}
