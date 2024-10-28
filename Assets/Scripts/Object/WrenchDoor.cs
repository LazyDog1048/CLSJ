using ui;
using UnityEngine;

namespace game
{
    public class WrenchDoor : LockedRoomDoor
    {
        [SerializeField]
        private bool lockSide;
        
        public override void PressE()
        {
            if(isOpened)
                Open();
            else if (!lockSide && CheckKey())
            {
                UnlockDoor();
                GameManager.Instance.CheckDoor(this);
                DescriptionUi.Instance.ShowDescription(UnlockDescription);
            }
            else
            {
                DescriptionUi.Instance.ShowDescription(LockedDescription);
            }
        }
    }
    
}
