using UnityEngine;

namespace EquipmentSystem
{
    [CreateAssetMenu(fileName = "ArmorSo", menuName = "Data/ArmorSo", order = 11)]
    public class ArmorSo : EquipmentSo
    {
        public override EquipmentType equipmentType => EquipmentType.Armor;
    }
    
}
