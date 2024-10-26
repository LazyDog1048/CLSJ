using data;
using EquipmentSystem;
using game;
using Player;
using tool;
using UnityEngine;

namespace Enemy
{
    public class GunEnemy : BaseEnemy
    {
        [SerializeField]
        private float range = 10;
        
        [SerializeField]
        private GunData gunData;
        // [SerializeField]
        // private Bullet_Data = 10;
        
        private GunObject gunObject;
        private EnemyGun _enemyGun;
        private Transform hand;
        private Transform gun;
        private SpriteRenderer gunSprite;
        private Transform shotPoint;
        private Vector3 lastPlayerPos;
        protected override void OnAwake()
        {
            base.OnAwake();
            gunObject = transform.GetComponentInChildren<GunObject>();
            gunObject.Init();
            gunObject.ReloadGun(gunData);
            hand = transform.Find("Hand");
            gun = hand.Find("Gun");
            gunSprite = gun.GetComponent<SpriteRenderer>();
            shotPoint = gun.Find("ShotPoint");

            switch (gunData.shotType)
            {
                case ShotType.ShotGun:
                    _enemyGun = new EnemyShotGun(this, gunObject, shotPoint, gunData);
                    break;
                default:
                    _enemyGun = new EnemyGun(this, gunObject, shotPoint, gunData);
                    break;
            }
        }
        
        
        protected override void EnemyUpdate()
        {
            if(CurState == EnemyState.Dead)
                return;
            Debug.Log(enemyAttacker.playerEnter);
            if (enemyAttacker.CanAttack)
            {
                SeenPlayer = true;
                lastPlayerPos = playerPos;
                EnemyAttack();
            }
            else if (SeenPlayer)
            {
                CurState = enemyAttacker.isAttackCd? EnemyState.WaitIdle : EnemyState.Attack;
                // var angle = GetAngle.Angle(lastPlayerPos, transform.position);
                // FaceMouse(angle);
            }
            else
            {
                EnemyPatrol();
            }
        }
        
        protected override void EnemyAttack()
        {
            var angle = GetAngle.Angle(playerPos, transform.position);
            FaceMouse(angle);
            if (transform.DisLongerThan(playerPos, enemyParameter.AttackRange))
            {
                CurState = EnemyState.WalkToPlayer;
                enemyMove.Move(playerPos);
            }
            else
            {
                enemyMove.faceDir.FaceToTarget(playerPos);
                CurState = enemyAttacker.isAttackCd? EnemyState.Idle : EnemyState.Attack;
            }
        }
        
        private void FaceMouse(float angle)
        {
            switch (VectorThing.WatchToTargetTwoDir(angle))
            {
                case FaceDir.Right:
                    gunSprite.sortingOrder = 1;
                    gunSprite.flipY = false;
                    break; 
                case FaceDir.Left:
                    gunSprite.sortingOrder = -1;
                    gunSprite.flipY = true;
                    break;
            }
            hand.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        protected override void AttackTrigger()
        {
            _enemyGun.CheckFire(playerPos);
        }
    }
    
}
