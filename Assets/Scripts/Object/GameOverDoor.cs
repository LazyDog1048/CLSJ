using EquipmentSystem;
using ui;
using UnityEngine;

namespace game
{
    public class GameOverDoor : LockedRoomDoor
    {
        protected override void Open()
        {
            GameOverPanel.Instance.OpenDoor();
        }
    }
    
}
