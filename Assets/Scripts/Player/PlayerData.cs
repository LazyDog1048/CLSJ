using System.Collections.Generic;
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
        public int health = 100;
        public float speed = 10;
        public int runRate = 30;

        public int stamina = 50;
        public Vector2 staminaResume = new Vector2(1, 0.5f);
        public Vector2 runStaminaConsume = new Vector2(1, 0.1f);
        
        public float runStepRate = 0.2f;
        public float walkStepRate = 0.5f;

        public FxAudioSourceClip hitClip;
        public List<FxAudioSourceClip> footStepClips;
        
        public void PlayFootStep()
        {
            if (footStepClips == null || footStepClips.Count == 0)
                return;
            footStepClips[Random.Range(0, footStepClips.Count)].PlayClip();
        }
    }
    
    public class PlayerParameter
    {
        public int health;
        
        public float speed;
        public int runRate;
        
        
        public int stamina;
        
        public Vector2 staminaResume;
        public Vector2 runStaminaConsume;
        public PlayerParameter(PlayerData data)
        {
            health = data.health;
            speed = data.speed;
            runRate = data.runRate;

            stamina = data.stamina;
            staminaResume = data.staminaResume;
            runStaminaConsume = data.runStaminaConsume;
        }
    }
    
    
    
}
