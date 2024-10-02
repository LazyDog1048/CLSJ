using System.Collections;
using System.Collections.Generic;
using game;
using other;
using tool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Gun : AbstractComponent
    {
        private PlayerController playerController;
        protected Transform hand;
        protected Transform body;
        protected SpriteRenderer gunSprite;
        public float angle { get; set; }
        
        
        public Gun(PlayerController mono) : base(mono)
        {
            playerController = mono;
            hand = mono.transform.Find("Hand");
            body = mono.transform.Find("Body");
            gunSprite = hand.GetComponentInChildren<SpriteRenderer>();
        }
        
        public void MouseMove(InputAction.CallbackContext context)
        {
            angle = GetAngle.Angle(GameCursor.Instance.transform.position, transform.position);
            hand.rotation = Quaternion.Euler(0, 0, angle);
            FaceMouse();
        }
        
        public void UpdateFieldOfView()
        {
            playerController.fieldOfView.SetAimDirection(angle + 90);
            playerController.fieldOfView.SetStartPos(hand.position);
        }
        private void FaceMouse()
        {
            switch (VectorThing.WatchToTargetTwoDir(angle))
            {
                case FaceDir.Up:
                case FaceDir.Down:
                    break;
                case FaceDir.Right:
                    gunSprite.sortingOrder = 1;
                    body.localScale = new Vector3(1, 1, 1);
                    break; 
                case FaceDir.Left:
                    gunSprite.sortingOrder = -1;
                    body.localScale = new Vector3(-1, 1, 1);
                    break;
            }
        }
    }
    
}
