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
        private BulletData bullet;

        private Transform ShotPoint_1;
        private Transform ShotPoint_2;
        public float angle { get; set; }
        
        
        public Gun(PlayerController mono) : base(mono)
        {
            playerController = mono;
            hand = mono.transform.Find("Hand");
            body = mono.transform.Find("Body");
            
            ShotPoint_1 = hand.Find("Gun").Find("ShotPoint_1");
            ShotPoint_2 = hand.Find("Gun").Find("ShotPoint_2");
            
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
        
        public void Shot(InputAction.CallbackContext context)
        {
            ShotBullet<Bullet>(playerController.BulletData,ShotPoint_1.position,ShotPoint_2.position);
        }
        
        public T ShotBullet<T>(BulletData bulletData,Vector3 startPos,Vector3 targetPos) where T:Bullet 
        {
            T baseBullet = Load<T>(bulletData);
            baseBullet.BulletPrepare(startPos,targetPos);
            return baseBullet;
        }
        
        public static T Load<T>(BulletData data) where T:Bullet
        {
            string path = $"Prefab/Bullet/{data.name}";
            return PoolManager.Instance.PopObj<T>(data.name,path);
        }
    }
    
}
