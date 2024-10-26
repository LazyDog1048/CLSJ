using System;
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
        [SerializeField]
        private RoomDoor startRoomDoor;
        
        
        public Room lastRoom { get; set; }
        
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
            BackToRoom();
        }
        public void CheckDoor(RoomDoor roomDoor)
        {
            lastRoomDoor = roomDoor;
            lastRoom = roomDoor.thisRoom;

            beforeEnterPackageData = JsonUtility.ToJson(DataManager.Instance.LocalPackageThing);
            beforeEnterPlayerData = JsonUtility.ToJson(DataManager.Instance.LocalPlayerDataThing);
        }

        public void BackToRoom()
        {
            DataManager.Instance.LocalPackageThing = JsonUtility.FromJson<LocalPackageThing>(beforeEnterPackageData);
            DataManager.Instance.LocalPlayerDataThing = JsonUtility.FromJson<LocalPlayerDataThing>(beforeEnterPlayerData);
            // DataManager.Instance.LocalPlayerDataThingHandler.SetData(JsonUtility.FromJson<LocalPlayerDataThing>(beforeEnterPlayerData));
            lastRoomDoor.ResumePlayer();
        }
    }
    
}
