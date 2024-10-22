using UnityEngine;

namespace Enemy 
{
    [CreateAssetMenu(fileName = "EnemySoData", menuName = "Data/EnemySoData")]
    public class EnemySoData : ScriptableObject
    {
        public string Name;
        public float Speed;
        public int Health;
        public float Damage;
        public float FindRange = 10;
        public float AttackRange = 2;

        public float AttackInterval;
    }


    public class EnemyParameter
    {
        public string Name;
        public float Speed;
        public int Health;
        public float Damage;
        public float FindRange;
        public float AttackRange;
        public float AttackInterval;
        
        public EnemyParameter(EnemySoData data)
        {
            Name = data.Name;
            Speed = data.Speed;
            Health = data.Health;
            Damage = data.Damage;
            AttackRange = data.AttackRange;
            FindRange = data.FindRange;
            AttackInterval = data.AttackInterval;
        }
    }
    
}
