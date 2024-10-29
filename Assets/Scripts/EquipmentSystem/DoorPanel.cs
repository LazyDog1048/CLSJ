using game;
using UnityEngine;

namespace ui
{
    public class DoorPanel : SingletonPanel<DoorPanel>
    {
        public DoorUi DoorUi;
        public static DoorPanel Load()
        {
            return Create(UiObjRefrenceSO.Instance.DoorPanelObj);
        }
        public DoorPanel(Transform trans) : base(trans)
        {
            PanelType = PanelType.PauseGame;
            DoorUi = trans.GetComponentInChildren<DoorUi>();
        }

        public void OpenDoor(RoomDoor roomDoor)
        {
            Show();
            DoorUi.SetRoomDoor(roomDoor);
            GameManager.Instance.DelayRealTimeExecute(6.3f,Hide);
        }
        
    }
       
}
