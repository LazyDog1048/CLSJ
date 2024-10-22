using UnityEngine;

namespace Player
{
    public class PlayerStamina:AbstractComponent
    {
        
        private int _currentStamina;

        public int CurrentStamina
        {
            get => _currentStamina;
            private set
            {
                if (value > MaxStamina)
                {
                    _currentStamina = MaxStamina;
                }
                else if (value < 0)
                {
                    _currentStamina = 0;
                }
                else
                {
                    _currentStamina = value;
                }
            }
        }
      
        
        public int MaxStamina { get;private set; }
        
        public Vector2 staminaResume { get;private set; }
        public Vector2 runStaminaConsume { get;private set; }
        
        // public bool StaminaLock { get;private set; }
        
        private PlayerController playerController;

        public bool CanConsume => CurrentStamina <= MaxStamina;
        // public bool CanRun => !StaminaLock && CurrentStamina > 0;
        public bool CanRun =>  CurrentStamina > 0;
        public PlayerStamina(PlayerController mono) : base(mono)
        {
            playerController = mono;
            MaxStamina = playerController.playerParameter.stamina;
            CurrentStamina = MaxStamina;
            staminaResume = playerController.playerParameter.staminaResume;
            runStaminaConsume = playerController.playerParameter.runStaminaConsume;
            Debug.Log($"Stamina:{CurrentStamina}");
        }

        public void StartRunConsumeStamina()
        {
            playerController.playerMove.PlayerRunStart();
            mono.LoopDelayExecute(runStaminaConsume.y,()=>!playerController.isRun,() =>
            {
                if (CurrentStamina >= runStaminaConsume.x)
                {
                    CurrentStamina -= (int)runStaminaConsume.x;
                }
                PlayerUiPanel.Instance.UpdateStaminaBar(CurrentStamina,MaxStamina);
            });
        }

        public void StopRunConsumeStamina()
        {
            if (CurrentStamina <= 0)
            {
                CurrentStamina = 0;
                // mono.WaitExecute(()=>CurrentStamina >= MaxStamina,() =>
                // {
                //     StaminaLock = false;
                // });
            }
            playerController.playerMove.PlayerRunComplete();
            mono.LoopDelayExecute(staminaResume.y,()=>!CanConsume || playerController.isRun,StaminaResume);
        }
        
        private void StaminaResume()
        {
            CurrentStamina += (int)staminaResume.x;
            PlayerUiPanel.Instance.UpdateStaminaBar(CurrentStamina,MaxStamina);
        }
    }
    
}
