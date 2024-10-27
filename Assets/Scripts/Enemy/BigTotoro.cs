using game;
using Player;
using UnityEngine;

namespace Enemy
{
    public class BigTotoro : Enemy_Dash
    {
        private LightObj lightObj;
        protected override void OnAwake()
        {
            base.OnAwake();
            lightObj = GetComponentInChildren<LightObj>();
            lightObj.lightEnter.AddListener(LightEnter);
        }

        private void LightEnter()
        {
            if (PlayerController.Instance.playerEquipment.lamp.gunData.Name.Equals("StrongLamp"))
            {
                CurState = EnemyState.Dead;
                lightObj.gameObject.SetActive(false);
            }
        }
        protected override void EnemyAttack()
        {
            if (!isEnterAttack)
            {
                direction = (playerPos - centerPosition).normalized;
                dashMove.faceDir.FaceToTarget(playerPos);
                CurState = EnemyState.Transform;
                isEnterAttack = true;
            }
        }
        
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
                    CurState = EnemyState.Idle;
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
    
}
