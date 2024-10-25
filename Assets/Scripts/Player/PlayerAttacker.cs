using Enemy;
using EquipmentSystem;
using plug;
using UnityEngine;

namespace Player
{
    public class PlayerAttacker : AbstractComponent
    {
        public PlayerParameter playerParameter;
        
        public int maxHp;
        private int _currentHp;

        public int currentHp
        {
            get => _currentHp;
            private set
            {
                if (value > maxHp)
                {
                    _currentHp = maxHp;
                }
                else if (value < 0)
                {
                    _currentHp = 0;
                }
                else
                {
                    _currentHp = value;
                }
                PlayerUiPanel.Instance.UpdateHpBar(currentHp,maxHp);
            }
        }
        private bool isInvincible;
        private float invincibleTime = 1f;

        private PlayerController playerController;
        
        public PlayerAttacker(PlayerController player,PlayerParameter playerParameter) : base(player)
        {
            playerController = player;
            this.playerParameter = playerParameter;
            maxHp = playerParameter.health;
            isInvincible = false;
            playerController.DelayExecute(0.1f, () =>
            {
                currentHp = maxHp;
                Debug.Log($"PlayerHp:{currentHp}");
            });

        }


        public void TakeDamage(BaseEnemy enemy)
        {
            Debug.Log("TakeDamage");
            if (isInvincible)
            {
                return;
            }
            currentHp -= enemy.enemyParameter.Damage;
            if (currentHp <= 0)
            {
                playerController.PlayerDead();
            }
            else
            {
                playerController.KnockBackPlayer(enemy.transform.position);
                EnterInvincible();
            }
        }
        
        private void EnterInvincible()
        {
            isInvincible = true;
            mono.DelayExecute(invincibleTime, () =>
            {
                isInvincible = false;
            });
        }
    }
}
