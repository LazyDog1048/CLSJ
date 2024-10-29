using game;
using item;
using Player;
using tool;
using UnityEngine;

namespace Enemy
{
    public class BigTotoro : Enemy_Dash
    {
        // private LightObj lightObj;
        private float distance = 5;
        protected override void OnAwake()
        {
            base.OnAwake();
            lightObj = GetComponentInChildren<LightObj>();
            lightObj.lightEnter.AddListener(LightEnter);
        }

        private void LightEnter()
        {
            if (PlayerController.Instance.playerEquipment.currentWeapon.gunData.Name.Equals("StrongLamp"))
            {
                if (!centerPosition.DisLongerThan(playerPos, distance) && CurState != EnemyState.Dead)
                {
                    CurState = EnemyState.Dead;
                    Debug.Log("Dead");
                    lightObj.enabled = false;
                }
            }
        }
        protected override void EnemyAttack()
        {
            PlayerFindClip();
            if (!isEnterAttack && !enemyAttacker.isAttackCd && !transform.DisLongerThan(playerPos, enemyParameter.AttackRange))
            {
                direction = (playerPos - centerPosition).normalized;
                dashMove.faceDir.FaceToTarget(playerPos);
                CurState = EnemyState.Transform;
                isEnterAttack = true;
            }
            else
            {
                CurState = EnemyState.WalkToPlayer;
                enemyMove.Move(playerPos);
            }
        }
        
        // protected override void EnemyAttack()
        // {
        //     if (!isEnterAttack)
        //     {
        //         direction = (playerPos - centerPosition).normalized;
        //         dashMove.faceDir.FaceToTarget(playerPos);
        //         CurState = EnemyState.Transform;
        //         isEnterAttack = true;
        //     }
        // }


        public override void AnimatorStateComplete()
        {
            switch (CurState)
            {
                case EnemyState.Transform:
                    CurState = EnemyState.Alert;
                    break;
                case EnemyState.Alert:
                    break;
                case EnemyState.Recover:
                    CurState = EnemyState.Idle;
                    isEnterAttack = false;
                    break;
                case EnemyState.Attack:
                    break;
                case EnemyState.Dead:
                    FxPlayer.PlayFx("Fx_EnemyDeath", centerPosition);
                    break;
            }
        }
    }
    
}
