using System;
using EquipmentSystem;
using game;
using item;
using Player;
using plug;
using tool;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Enemy
{
    public class BaseEnemy : MonoPoolObj,IHitObj,IAnimatorController
    {
        [SerializeField]
        public LayerMask playerMask;
        
        public EnemySoData enemySoData;
        private EnemyParameter enemyParameter;
        private Rigidbody2D rigidbody2D;
        private EnemyMove enemyMove;
        private EnemyAnimator enemyAnimator;
        private EnemyAttacker enemyAttacker;
        public EnemyState CurState
        {
            get => enemyAnimator.CurState;
            set => enemyAnimator.SetAnim(value);
        }
        private LightObj lightObj;
        public Vector3 enemyPosition=>center.position;
        public override string poolId => enemySoData.Name;
        
        

        private float knockBackTime = 0.2f;
        private Vector3 playerPos => PlayerController.Instance.transform.position;
        private Transform center;
        protected override void OnAwake()
        {
            base.OnAwake();
            rigidbody2D = GetComponent<Rigidbody2D>();
            lightObj = GetComponent<LightObj>();
            center = transform.Find("Center");
            enemyParameter = new EnemyParameter(enemySoData);
            enemyMove = new EnemyMove(this,enemyParameter.Speed,this);
            enemyAnimator = new EnemyAnimator(this);
            enemyAttacker = new EnemyAttacker(this, enemyParameter);

            
        }

        private void Update()
        {
            if(CurState == EnemyState.Dead)
                return;
            if (enemyAttacker.CanAttack)
            {
                if (transform.DisLongerThan(playerPos, enemyParameter.AttackRange))
                {
                    CurState = EnemyState.WalkToPlayer;
                    enemyMove.Move(playerPos);
                }
                else
                {
                    CurState = enemyAttacker.isAttackCd? EnemyState.Idle : EnemyState.Attack;
                }
            }
            else
            {
                CurState = EnemyState.Idle;
            }
            
        }

        private void AttackTrigger()
        {
            
        }

        private void CheckPlayer()
        {
            
        }




        public void HitObj(Bullet bullet)
        {
            float force = 1;
            rigidbody2D.AddForce(bullet.currentDir * force,ForceMode2D.Impulse);
            FxPlayer.PlayFx("Fx_Gun_Hit", enemyPosition);
            this.DelayExecute(knockBackTime,KnockBackComplete);
            Debug.Log(bullet.gun.Damage);
            enemyAttacker.currentHp -= bullet.gun.Damage;
            if (enemyAttacker.currentHp <= 0)
                CurState = EnemyState.Dead;
        }

        private void KnockBackComplete()
        {
            rigidbody2D.linearVelocity = Vector2.zero;
        }

        public void AnimatorStateEnter()
        {
            switch (CurState)
            {
                case EnemyState.Attack:
                    enemyAttacker.AttackEnterCd();
                    break;
                case EnemyState.Dead:
                    FxPlayer.PlayFx("Fx_EnemyDeath", enemyPosition);
                    break;
            }
        }

        public void AnimatorStateComplete()
        {
            switch (CurState)
            {
                case EnemyState.Attack:
                    CurState = EnemyState.Idle;
                    break;
                case EnemyState.Dead:
                    CurState = EnemyState.Idle;
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
    
}
