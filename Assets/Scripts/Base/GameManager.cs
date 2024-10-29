using System;
using System.Collections.Generic;
using data;
using EquipmentSystem;
using GridSystem;
using other;
using Player;
using ui;
using UnityEngine;

namespace game
{
    public class GameManager : KeepMonoSingleton<GameManager>
    {
        // []
        // public FxAudioSourceClip AmbienceAudio
        [SerializeField]
        private RoomDoor startRoomDoor;

        private List<Room> rooms;

        public Room lastRoom => lastRoomDoor.thisRoom;
        public Room thisRoom => lastRoomDoor.targetRoom;
        public RoomDoor lastRoomDoor{ get; set; }
        
        // private LocalPackageThing beforeEnterPackageData;
        // private LocalPlayerDataThing beforeEnterPlayerData;
        private string beforeEnterPackageData;
        private string beforeEnterPlayerData;
        protected override void Awake()
        {
            base.Awake();
            DataManager.Instance.Create();
            ResourcesDataManager.Instance.Create();
            InputManager.Instance.Create();
            GamePlay_InputAction.Instance.Create();
            Ui_InputAction.Instance.Create();
            CameraManager.Instance.Load();
            LayerPanel.Load();
            PlayerUiPanel.Load();
            Package_Panel.Load();
            DoorPanel.Load();
            Map_Panel.Load();
            PlayerController.Instance.PlayerInit();
            
            
            rooms = new List<Room>();
            
            foreach (var room in GetComponentsInChildren<Room>())
            {
                rooms.Add(room);
            }

            foreach (var room in rooms)
            {
                room.ExitRoom();
            }
        }

        private void Start()
        {
            StartGame();
        }

        public void Pause(bool pause)
        {
            if (pause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        
        public void StartGame()
        {
            CheckDoor(startRoomDoor);
            EnterTargetRoom(startRoomDoor);
            
        }
        public void CheckDoor(RoomDoor roomDoor)
        {
            lastRoomDoor = roomDoor;

            beforeEnterPackageData = JsonUtility.ToJson(DataManager.Instance.LocalPackageThing);
            beforeEnterPlayerData = JsonUtility.ToJson(DataManager.Instance.LocalPlayerDataThing);
        }

        public void EnterTargetRoom(RoomDoor roomDoor)
        {
            DataManager.Instance.LocalPackageThing = JsonUtility.FromJson<LocalPackageThing>(beforeEnterPackageData);
            DataManager.Instance.LocalPlayerDataThing = JsonUtility.FromJson<LocalPlayerDataThing>(beforeEnterPlayerData);
            // DataManager.Instance.LocalPlayerDataThingHandler.SetData(JsonUtility.FromJson<LocalPlayerDataThing>(beforeEnterPlayerData));

            if (lastRoomDoor != null)
            {
                lastRoom.ExitRoom();    
            }
            this.lastRoomDoor = roomDoor;
            PlayerController.Instance.EnterRoom(roomDoor);
            roomDoor.targetRoom.EnterRoom();
        }
        
    }
    
}
