using buff;
using data;
using EquipmentSystem;
using other;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData :ScriptableObject 
    {
        public float speed = 10;
        public int runRate = 30;

        public int stamina = 50;
        public Vector2 staminaResume = new Vector2(1, 0.5f);
        public Vector2 runStaminaConsume = new Vector2(1, 0.1f);
    }
    
    public class PlayerParameter
    {
        public float speed;
        public int runRate;
        
        
        public int stamina;
        
        public Vector2 staminaResume;
        public Vector2 runStaminaConsume;
        public PlayerParameter(PlayerData data)
        {
            speed = data.speed;
            runRate = data.runRate;

            stamina = data.stamina;
            staminaResume = data.staminaResume;
            runStaminaConsume = data.runStaminaConsume;
        }
    }
    
    
    
}
