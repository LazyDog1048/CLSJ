using UnityEngine;

namespace Enemy 
{
    [CreateAssetMenu(fileName = "EnemySoData", menuName = "Data/EnemySoData")]
    public class EnemySoData : ScriptableObject
    {
        public string Name;
        public float Speed;
        public float Health;
        public float Damage;
        public float AttackRange;
    }


    public class EnemyParameter
    {
        public string Name;
        public float Speed;
        public float Health;
        public float Damage;
        public float AttackRange;

        public EnemyParameter(EnemySoData data)
        {
            Name = data.Name;
            Speed = data.Speed;
            Health = data.Health;
            Damage = data.Damage;
            AttackRange = data.AttackRange;
        }
    }
    
}
