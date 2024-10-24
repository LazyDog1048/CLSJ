using data;
using EquipmentSystem;
using game;
using plug;
using tool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    
    public class PlayerController : MonoBehaviour, IAnimatorController
    {
        [SerializeField]
        private PlayerData playerData;

        public BulletData BulletData;
        public PlayerParameter playerParameter { get; set; }
        
        public static PlayerController Instance;
        private PlayerAnimController animator { get;set; }
        public PlayerMove playerMove { get; set; }
        private PlayerStamina playerStamina { get; set; }
        public PlayerHand PlayerHand { get; set; }
        public PlayerEquipment playerEquipment { get; set; }
        
        private BaseGun gun_1;
        private BaseGun gun_2;

        private GunObject gun;
        public Transform shotCenter { get; set; }
        public float angle => PlayerHand.angle;
        
        public bool isRun => PlayerState == PlayerState.Run;
        
        
        private bool isPressShift;
        public bool isPressMove{ get; set; }
        // public PlayerState previousState;
        public PlayerState PlayerState
        {
            get => animator.CurState;
            protected set
            {
                PlayerState previousState = PlayerState;
                animator.SetAnim(value);
                if(value == PlayerState && previousState != PlayerState)
                    StateChange(previousState,PlayerState);
            }
        }


        // private GamePlayInput gamePlayInput;
        #region UnityAction

        protected void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            Instance = this;
            gun = transform.Find("Hand").Find("Gun").GetComponent<GunObject>();
            shotCenter = transform.Find("ShotCenter");
            gun.Init();
            playerParameter = new PlayerParameter(playerData);
            animator = new PlayerAnimController(this);
            playerMove = new PlayerMove(this);
            playerStamina = new PlayerStamina(this);
            PlayerHand = new PlayerHand(this);
            playerEquipment = new PlayerEquipment(this);
            GamePlay_InputAction.Instance.PlayerRegisterAction(OnMove,CursorMoveEvent,RightMouse,PressShift,PressTab,PressR,PressE,PressQ,PressF);
            GamePlay_InputAction.Instance.PlayerRegisterNumAction(Press1,Press2,Press3,Press4,Press5,Press6,Press7);
            GamePlay_InputAction.Instance.ConfirmUiAction(LeftMouse);
        }

        #endregion

        #region Listener

        private void Update()
        {
            if (playerMove.CanMove)
            {
                PlayerState = isPressShift && playerStamina.CanRun ? PlayerState.Run : PlayerState.Walk;
            }
            else
            {
                PlayerState = PlayerState.Idle;    
            }
            
            playerMove.Move(GamePlay_InputAction.moveDir);
            PlayerHand.UpdateFieldOfView();
        }

        private void StateChange(PlayerState previousState,PlayerState curState)
        {
            if (curState == PlayerState.Run)
            {
                playerStamina.StartRunConsumeStamina();
            }
            else if(previousState == PlayerState.Run)
            {
                playerStamina.StopRunConsumeStamina();
            }
        }

        public BaseGun ChangeGun_1()
        {
            var weapon = LocalPlayerDataThing.GetData().weapon_1;
            
            if (weapon.Name.Equals(""))
                return gun_1;
            if(gun_1 != null && gun_1.WeaponData == weapon)
                return gun_1;
            gun_1 = ChangeGun(weapon);
            return gun_1;
        }
        
        public BaseGun ChangeGun_2()
        {
            var weapon = LocalPlayerDataThing.GetData().weapon_2;
            
            if (weapon.Name.Equals(""))
                return gun_2;
            if(gun_2 != null && gun_2.WeaponData == weapon)
                return gun_2;
            gun_2 = ChangeGun(weapon);
            return gun_2;
        }
        
        public BaseGun ChangeGun(WeaponData weapon)
        {
            GunData gunData = ResourcesDataManager.GetPackageItemSoData(weapon.Name) as GunData;
            // GameObject gunObj = GameObject.Instantiate(gunData.prefab,gun);
            // gunObj.transform.localPosition = Vector3.zero;
            // gunObj.transform.localRotation = Quaternion.identity;
            // gunObj.transform.localScale = Vector3.one;
            switch (gunData.shotType)
            {
                case ShotType.Triple:
                    return new TripleGun(this,gun,shotCenter,gunData,weapon);
                case ShotType.ShotGun:
                    return new ShotGun(this,gun,shotCenter,gunData,weapon);
                default:
                    return new BaseGun(this,gun,shotCenter,gunData,weapon);
            }
        }
        private void OnMove(InputAction.CallbackContext context)
        {
            // Debug.Log("move");
            // if (context.phase == InputActionPhase.Performed && playerMove.CanMove)
            // {
            //     PlayerState = isPressShift && playerStamina.CanRun ? PlayerState.Run : PlayerState.Walk;
            // }
            // else if (context.phase == InputActionPhase.Canceled)
            // {
            //     PlayerState = PlayerState.Idle;    
            // }
            
            if (context.phase == InputActionPhase.Performed)
            {
                isPressMove = true;
                
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                isPressMove = false;
            }
        }

        private void CursorMoveEvent(InputAction.CallbackContext context)
        {
            PlayerHand.MouseMove(context);
        }
        
        private void LeftMouse(InputAction.CallbackContext context)
        {
            playerEquipment.currentWeapon?.GunFire(context);
        }
        
        private void RightMouse(InputAction.CallbackContext context)
        {
            PlayerHand.RightMouse(context);
        }
        
        private void PressR(InputAction.CallbackContext context)
        {
            playerEquipment.currentWeapon?.ReloadBullet(context);
        }
        
        private void PressShift(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                isPressShift = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                isPressShift = false;
            }
        }

        private void PressTab(InputAction.CallbackContext context)
        {
            // var list = transform.position.FindCircleAllCollider<Container>(3,LayerMask.GetMask("PlayerBody"),"Container");
            // if(list.Count <=0)
            //     return;
            // list.SortByDis(transform.position);
            // list[0].OpenOrClose();
        }
        
        private void PressE(InputAction.CallbackContext context)
        {   
            
           
        }

        private void PressQ(InputAction.CallbackContext context)
        {
            
        }
        
        private void PressF(InputAction.CallbackContext context)
        {
            
        }
        
        private void Press1(InputAction.CallbackContext context)
        {
            playerEquipment.SwitchToWeapon1();
            
        }
        
        private void Press2(InputAction.CallbackContext context)
        {
            playerEquipment.SwitchToWeapon2();
        }
        
        private void Press3(InputAction.CallbackContext context)
        {
            
        }
        
        private void Press4(InputAction.CallbackContext context)
        {
            
        }
        
        private void Press5(InputAction.CallbackContext context)
        {
            
        }
        
        private void Press6(InputAction.CallbackContext context)
        {
            
        }
        
        private void Press7(InputAction.CallbackContext context)
        {
            
        }
        #endregion


        public void PlayerStateChange(PlayerState state)
        {
            PlayerState = state;
        }
        public void PlayerWalkToTarget(Vector3 targetPos,UnityAction Complete)
        {
            playerMove.AutoMove(targetPos,Complete);
        }

        public void PlayerRelease()
        {
            transform.SetParent(null);
            PlayerState = PlayerState.Idle;
        }

        public void KnockBackPlayer(Vector3 point)
        {
            playerMove.AddForce(point,0.5f);
        }

        
        private void PlayerInvincible()
        {
            // playerMove.MoveEnable(false);
            // playerStamina.StopRunConsumeStamina();
        }
        #region IAnimController
        public virtual void AnimatorStateEnter()
        {
            
        }
        public virtual void AnimatorStateComplete()
        {
        
        }
        #endregion
    }

}
