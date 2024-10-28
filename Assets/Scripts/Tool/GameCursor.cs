using EquipmentSystem;
using Player;
using tool;
using UnityEngine;

namespace other
{
    public class GameCursor : Mono_Singleton<GameCursor>
    {
        // private GameObject body;
        private GameObject range;
        private bool cursorClicked;

        private Transform up;
        private Transform down;
        private Transform left;
        private Transform right;

        
        private PlayerGun playerGun => PlayerController.Instance.playerEquipment.currentWeapon;

        protected override void Awake()
        {
            base.Awake();
            Cursor.visible = false;
            range = transform.Find("Range").gameObject;
            up = range.transform.Find("Up");
            down = range.transform.Find("Down");
            left = range.transform.Find("Left");
            right = range.transform.Find("Right");
        }
        
        public void SetPlayerController(PlayerController playerController)
        {
            // this.playerController = playerController;
        }
        
        public void SwitchMode(bool isAiming)
        {
            Cursor.visible = !isAiming;
            range.SetActive(isAiming);
        }

        private void Update()
        {
            transform.position = GetMousePos.GetMousePosition();
            AimingChange(playerGun.range);
        }

        public void AimingChange(float angleA)
        {
            float lengthA = angleA / 10f;
            up.transform.localPosition =  Vector3.up * lengthA;
            down.transform.localPosition =  Vector3.down * lengthA;
            left.transform.localPosition = Vector3.left * lengthA;      
            right.transform.localPosition = Vector3.right * lengthA;
        }
    }

}
