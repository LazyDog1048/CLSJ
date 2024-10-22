using EquipmentSystem;
using Player;
using tool;
using UnityEngine;

namespace other
{
    public class GameCursor : Mono_Singleton<GameCursor>
    {
        private GameObject body;
        private bool cursorClicked;

        private Transform up;
        private Transform down;
        private Transform left;
        private Transform right;

        private PlayerController playerController;
        private BaseGun playerGun => playerController.playerEquipment.currentWeapon;
        
        protected override void Awake()
        {
            base.Awake();
            playerController = PlayerController.Instance;
            body = transform.Find("Body").gameObject;
            var range = transform.Find("Range").gameObject;
            up = range.transform.Find("Up");
            down = range.transform.Find("Down");
            left = range.transform.Find("Left");
            right = range.transform.Find("Right");
        }

        private void Update()
        {
            transform.position = GetMousePos.GetMousePosition();
            float lineLength = Vector3.Distance(playerController.shotCenter.position, transform.position);
           
            AimingChange(playerGun.range,lineLength);
        }

        public void AimingChange(float angleA,float lengthB)
        {
            float angleRadians = angleA * Mathf.Deg2Rad;
            float lengthC = lengthB / Mathf.Cos(angleRadians);
            float lengthA = lengthC * Mathf.Sin(angleRadians);
            
            up.transform.localPosition =  Vector3.up * lengthA;
            down.transform.localPosition =  Vector3.down * lengthA;
            left.transform.localPosition = Vector3.left * lengthA;      
            right.transform.localPosition = Vector3.right * lengthA;
        }
        
        public void RenderTriangle(float angleA,float lengthB)
        {
            float angleRadians = angleA * Mathf.Deg2Rad;
            float lengthC = lengthB / Mathf.Cos(angleRadians);
            float lengthA = lengthC * Mathf.Sin(angleRadians);

            Debug.DrawRay(playerController.shotCenter.position,Vector3.right * lengthB,Color.green);
            Debug.DrawRay(playerController.shotCenter.position,Vector3.right.Rota2DAxis(angleA) * lengthC,Color.red);
            Debug.DrawRay(transform.position,Vector3.up * lengthA,Color.blue);
        }
        
    }

}
