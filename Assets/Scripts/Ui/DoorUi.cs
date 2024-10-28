using data;
using game;
using UnityEngine;

namespace ui
{
    public class DoorUi : MonoBehaviour
    {
        [SerializeField]
        private FxAudioSourceClip handleAudio;
        [SerializeField]
        private FxAudioSourceClip doorAudio;
        
        private RoomDoor roomDoor;
        private void HandleAudio()
        {
            handleAudio.PlayClip();
        }
        
        private void DoorAudio()
        {
            doorAudio.PlayClip();
            roomDoor.MovePlayer();
        }
        
        public void SetRoomDoor(RoomDoor roomDoor)
        {
            this.roomDoor = roomDoor;
        }

        private void MovePlayer()
        {
            // roomDoor.MovePlayer();
        }
    }
    
}
