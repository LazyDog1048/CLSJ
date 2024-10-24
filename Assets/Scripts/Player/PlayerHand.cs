using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using EquipmentSystem;
using game;
using other;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerHand : AbstractComponent
    {
        private PlayerController playerController;
        protected Transform hand;
        protected Transform body;
        protected SpriteRenderer gunSprite;
        private BulletData bullet;
        private PlayerFlashlight flashlight;
        
        private bool isPressRightMouse;
        public float angle { get; set; }

        public GunParameter GunParameter;
        
        public bool isAiming => isPressRightMouse;

        public float minAimingShake => playerController.playerEquipment.minCalibration;
        public float aimingShake { get; set; }
        public float aimingTime = 0.5f;
        
        TweenerCore<float,float,FloatOptions> aimingShakeTween;
        public PlayerHand(PlayerController mono) : base(mono)
        {
            playerController = mono;
            hand = mono.transform.Find("Hand");
            body = mono.transform.Find("Body");
            gunSprite = hand.GetComponentInChildren<SpriteRenderer>();
            flashlight = hand.GetComponentInChildren<PlayerFlashlight>();
        }
        
        public void MouseMove(InputAction.CallbackContext context)
        {

            angle = GetAngle.Angle(GameCursor.Instance.transform.position, transform.position);
            hand.rotation = Quaternion.Euler(0, 0, angle);
            FaceMouse();
        }
        
        public void RightMouse(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                isPressRightMouse = true;
                if(aimingShakeTween!=null)
                    aimingShakeTween.Kill();
                
                aimingShakeTween = DOTween.To(() => aimingShake, x => aimingShake = x, minAimingShake, aimingTime).SetEase(Ease.Linear);
                
                flashlight.AimMode();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                isPressRightMouse = false;
                
                if(aimingShakeTween!=null)
                    aimingShakeTween.Kill();
                aimingShakeTween = DOTween.To(() => aimingShake, x => aimingShake = x, 0, aimingTime).SetEase(Ease.Linear);
                
                flashlight.NormalMode();
            }
        }
        
        public void UpdateFieldOfView()
        {
            // playerController.fieldOfView.SetAimDirection(angle);
            // playerController.fieldOfView.SetStartPos(hand.position);
        }
        
        
        private void FaceMouse()
        {
            switch (VectorThing.WatchToTargetTwoDir(angle))
            {
                case FaceDir.Right:
                    gunSprite.sortingOrder = 1;
                    gunSprite.flipY = false;
                    body.localScale = new Vector3(1, 1, 1);
                    break; 
                case FaceDir.Left:
                    gunSprite.sortingOrder = -1;
                    gunSprite.flipY = true;
                    body.localScale = new Vector3(-1, 1, 1);
                    break;
            }
        }
        
       
    }
    
}
