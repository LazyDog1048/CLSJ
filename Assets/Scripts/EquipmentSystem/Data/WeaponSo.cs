using UnityEngine;

namespace EquipmentSystem
{
    [CreateAssetMenu(fileName = "WeaponSo", menuName = "Data/WeaponSo", order = 11)]
    public class WeaponSo : EquipmentSo
    {
        public override EquipmentType equipmentType => EquipmentType.Weapon;
    }
    
}
