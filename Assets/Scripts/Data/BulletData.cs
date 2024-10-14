using UnityEngine;

namespace game
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Data/BulletData")]
    public class BulletData : ScriptableObject
    {
        public string name;
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
            name = data.name;
            speed = data.speed;
            stayTime = data.stayTime;
        }
    }
}
