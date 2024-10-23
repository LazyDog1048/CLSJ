using System;
using System.Collections.Generic;
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
        protected List<Vector3> PatrolPoints;
        protected int patrolIndex = 0;
        protected float patrolIdleTime = 2f;
        protected bool patrolLock = false;
        
        
        // protected Rigidbody2D rigidbody2D;
        public EnemySoData enemySoData;
        protected EnemyParameter enemyParameter;
        protected EnemyMove enemyMove;
        protected EnemyAnimator enemyAnimator;
        protected EnemyAttacker enemyAttacker;
        public EnemyState CurState
        {
            get => enemyAnimator.CurState;
            set => enemyAnimator.SetAnim(value);
        }
        private LightObj lightObj;
        public Vector3 enemyPosition=>center.position;
        public override string poolId => enemySoData.Name;

        
        
        private float knockBackTime = 0.2f;
        protected Vector3 playerPos => PlayerController.Instance.transform.position;
        protected Transform center;
        protected override void OnAwake()
        {
            base.OnAwake();
            // rigidbody2D = GetComponent<Rigidbody2D>();
            lightObj = GetComponent<LightObj>();
            center = transform.Find("Center");
            enemyParameter = new EnemyParameter(enemySoData);
            SetMove();
            enemyAnimator = new EnemyAnimator(this);
            enemyAttacker = new EnemyAttacker(this, enemyParameter);
            PatrolPoints = new List<Vector3>();
            patrolIndex = 0;
            var patrol = transform.Find("Patrol");
            for (int i = 0; i < patrol.childCount; i++)
            {
                PatrolPoints.Add(patrol.GetChild(i).position);
            }
        }

        protected virtual void SetMove()
        {
            enemyMove = new EnemyMove(this,enemyParameter.Speed,this);
        }

        private void Update()
        {
            EnemyUpdate();
        }
        
        protected virtual void EnemyUpdate()
        {
            if(CurState == EnemyState.Dead)
                return;
            if (enemyAttacker.CanAttack)
            {
                EnemyAttack();
            }
            else
            {
                EnemyPatrol();
            }
        }

        protected virtual void EnemyAttack()
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

        protected void EnemyPatrol()
        {
            if(patrolLock)
                return;
                
            if (transform.DisLongerThan(PatrolPoints[patrolIndex], 0.1f))
            {
                CurState = EnemyState.PatrolWalk;
                enemyMove.Move(PatrolPoints[patrolIndex]);
            }
            else
            {
                patrolLock = true;
                this.DelayExecute(patrolIdleTime, () =>
                {
                    patrolLock = false;
                });
                CurState = EnemyState.PatrolIdle;
                patrolIndex++;
                patrolIndex %= PatrolPoints.Count;
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
            enemyMove.AddForce(bullet.currentDir,knockBackTime);
            FxPlayer.PlayFx("Fx_Gun_Hit", enemyPosition);
            enemyAttacker.currentHp -= bullet.gun.Damage;
            if (enemyAttacker.currentHp <= 0)
                CurState = EnemyState.Dead;
        }

        public virtual void AnimatorStateEnter()
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

        public virtual void AnimatorStateComplete()
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
