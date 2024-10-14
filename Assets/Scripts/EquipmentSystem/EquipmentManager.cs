using other;
using UnityEngine;

namespace EquipmentSystem
{
    public class EquipmentManager : Class_Singleton<EquipmentManager>
    {
        public void GenerateRandomEquipment(Vector3 position,EquipmentDic equipmentDic)
        {
            var equipmentSo = EquipmentDic.GetRandomEquipment(equipmentDic);
            var equipmentItem = GameObject.Instantiate(equipmentSo.prefab);
            equipmentItem.transform.position = position;
        }  
    }
    
}
