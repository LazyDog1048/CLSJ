using System;
using game;
using UnityEngine;

namespace ui
{
    public class RoomUi : MonoBehaviour
    {
        [SerializeField]
        private string roomName;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void CheckRoom(Room room)
        {
            Debug.Log($"{roomName} {room.roomName}");
            gameObject.SetActive(roomName.Equals(room.roomName));
        }
    }

}
