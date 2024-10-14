using GridSystem;

namespace EquipmentSystem
{
    // [CreateAssetMenu(fileName = "EquipmentSo", menuName = "Data/EquipmentSo", order = 11)]
    public abstract class EquipmentSo : GridObjectSo
    {
        public Quality quality;
        public virtual EquipmentType equipmentType => EquipmentType.Weapon;
        public int level;
    }
    
    public enum Quality
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Accessory
    }
    
    
    public static class QualityExtension
    {
        public static float GetQualityMultiplier(this Quality quality)
        {
            switch (quality)
            {
                case Quality.Common:
                    return 1;
                case Quality.Uncommon:
                    return 1.1f;
                case Quality.Rare:
                    return 1.2f;
                case Quality.Epic:
                    return 1.3f;
                default:
                    return 1.4f;
            }
        }
    }
}
