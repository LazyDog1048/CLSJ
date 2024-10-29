using System.Collections.Generic;
using game;
using UnityEngine;

namespace ui
{
    public class Map_Panel : SingletonPanel<Map_Panel>
    {
        private List<RoomUi> roomUis;
        public static Map_Panel Load()
        {
            return Create(UiObjRefrenceSO.Instance.MapPanelObj);
        }
        public Map_Panel(Transform trans) : base(trans)
        {
            PanelType = PanelType.PauseGame;
            var rTransform = trans.Find("Room");
            roomUis = new List<RoomUi>();
            foreach (Transform child in rTransform)
            {
                roomUis.Add(child.GetComponent<RoomUi>());
            }
            // roomUis = trans.GetComponentsInChildren<RoomUi>();
        }


        protected override void OnShowAction()
        {
            UpdataRoom();
        }

        private void UpdataRoom()
        {
            foreach (var roomUi in roomUis)
            {
                roomUi.CheckRoom(GameManager.Instance.thisRoom);
            }
        }
    }
}


