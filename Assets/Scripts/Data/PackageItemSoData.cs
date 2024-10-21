using GridSystem;
using UnityEngine;

namespace data
{
    public enum  PackageItemType
    {
        Weapon,
        MeleeWeapon,
        Armor_Cap,
        Armor_Coat,
        Armor_LeftShoe,
        Armor_RightShoe,
        Spell,
        Shield,
        Accessory,
        Consumable,
        Package,
        Other
    }
    
    public enum Quality
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    [CreateAssetMenu(fileName = "PackageItemData", menuName = "Data/PackageItemData")]
    public class PackageItemSoData : GridObjectSo
    {
        public PackageItemType ItemType;
        public Quality quality;
        public int price;
        public float weight;
        
        public string description;
    }
    
}
