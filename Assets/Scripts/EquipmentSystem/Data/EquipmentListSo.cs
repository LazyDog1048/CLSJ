using System;
using data;

namespace EquipmentSystem
{

    [Serializable]
    public class EquipmentDic : SerializableDic<EquipmentSo, int>
    {
        public static EquipmentSo GetRandomEquipment(EquipmentDic equipmentDic)
        {
            var random = UnityEngine.Random.Range(0, 100);
            int count = 0;
            for(int i=0;i<equipmentDic.Count ;i++)
            {
                count += equipmentDic.Values[i];
                if (random < count)
                {
                    return equipmentDic.Keys[i];
                }
            }

            return null;
        }
    }
}
