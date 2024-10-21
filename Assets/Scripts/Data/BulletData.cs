using data;
using UnityEngine;

namespace EquipmentSystem
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Data/BulletData")]
    public class BulletData : PackageItemSoData
    {
        public float speed;
        public float stayTime;
    }
    
    public class BulletParameter
    {
        public string name;
        public float speed;
        public float stayTime;
        
        public BulletParameter(BulletData data)
        {
            name = data.Name;
            speed = data.speed;
            stayTime = data.stayTime;
        }
    }
}
