using UnityEngine;

namespace Enemy 
{
    [CreateAssetMenu(fileName = "EnemySoData", menuName = "Data/EnemySoData")]
    public class EnemySoData : ScriptableObject
    {
        public string Name;
        public float Speed;
        public int Health;
        public int Damage;
        public int DamageForce = 8;
        public float DamageForceTime = 0.5f;
        public int TouchDamage = 2;
        public int TouchForce = 5;
        public float TouchForceTime = 0.5f;
        public float FindRange = 10;
        public float AttackRange = 2;

        public float AttackInterval;
    }


    public class EnemyParameter
    {
        public string Name;
        public float Speed;
        public int Health;
        public int Damage;
        public int DamageForce;
        public float DamageForceTime;
        public int TouchDamage;
        public int TouchForce;
        public float TouchForceTime;
        public float FindRange;
        public float AttackRange;
        public float AttackInterval;
        
        public EnemyParameter(EnemySoData data)
        {
            Name = data.Name;
            Speed = data.Speed;
            Health = data.Health;
            Damage = data.Damage;
            DamageForce = data.DamageForce;
            DamageForceTime = data.DamageForceTime;
            TouchDamage = data.TouchDamage;
            TouchForce = data.TouchForce;
            TouchForceTime = data.TouchForceTime;
            AttackRange = data.AttackRange;
            FindRange = data.FindRange;
            AttackInterval = data.AttackInterval;
        }
    }
    
}
