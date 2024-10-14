using UnityEngine;

namespace EquipmentSystem
{
    [CreateAssetMenu(fileName = "AccessorySo", menuName = "Data/AccessorySo", order = 11)]
    public class AccessorySo : EquipmentSo
    {
        public override EquipmentType equipmentType => EquipmentType.Accessory;
    }
    
}
