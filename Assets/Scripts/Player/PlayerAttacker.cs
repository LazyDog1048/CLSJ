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

        private bool lockKnockBack;
        private float knockBackTime = 0.2f;
        private PlayerController playerController;
        
        public PlayerAttacker(PlayerController player,PlayerParameter playerParameter) : base(player)
        {
            playerController = player;
            this.playerParameter = playerParameter;
            maxHp = playerParameter.health;
            isInvincible = false;
            Check2DRange check2DRange = transform.Find("Body").GetComponent<Check2DRange>();
            check2DRange.Init(OnTriggerEnter2D,OnTriggerExit2D);
            playerController.DelayExecute(0.1f, () =>
            {
                currentHp = maxHp;
            });

        }


        public void TakeDamage(BaseEnemy enemy)
        {
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
                playerController.KnockBackPlayer(enemy.transform.position,enemy.enemyParameter.DamageForce);
                EnterInvincible();
            }
        }
        
        public void TakeDamage(Bullet bullet)
        {
            currentHp -= bullet.gunParameter.Damage;
            if (currentHp <= 0)
            {
                playerController.PlayerDead();
            }
            else if (!lockKnockBack)
            {
                playerController.KnockBackPlayer(bullet.transform.position,1);
                EnterLockKnockBack();
            }
        }
        
        public void Heal(int healValue)
        {
            currentHp += healValue;
        }
        
        private void EnterInvincible()
        {
            isInvincible = true;
            mono.DelayExecute(invincibleTime, () =>
            {
                isInvincible = false;
            });
        }

        private void EnterLockKnockBack()
        {
            lockKnockBack = true;
            mono.DelayExecute(knockBackTime, () =>
            {
                lockKnockBack = false;
            });
        }
        
        public void Resume()
        {
            currentHp = maxHp;
            isInvincible = false;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("EnemyBody"))
            {
                var enemy = col.GetComponentInParent<BaseEnemy>();
                if (enemy != null)
                {
                    TouchDamage(enemy);
                }
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
        }

        public void TouchDamage(BaseEnemy enemy)
        {
            if (isInvincible)
            {
                return;
            }
            currentHp -= enemy.enemyParameter.TouchDamage;
            if (currentHp <= 0)
            {
                playerController.PlayerDead();
            }
            else
            {
                playerController.KnockBackPlayer(enemy.transform.position,enemy.enemyParameter.TouchForce);
                EnterInvincible();
            }
        }
    }
}
