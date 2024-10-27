using System;
using System.Collections.Generic;
using data;
using EquipmentSystem;
using NUnit.Framework;
using Player;
using ui;
using UnityEngine;
using Random = UnityEngine.Random;

namespace game
{
    public class RoomDoor : SceneObject
    {
        public Room thisRoom { get;private set; }
        
        [SerializeField]
        public Room targetRoom;

        private Transform target;
        private Transform resume;



        protected override void Awake()
        {
            base.Awake();
            target = transform.Find("Target");
            resume = transform.Find("Resume");
            thisRoom = transform.parent.parent.GetComponent<Room>();
        }

        public override void PressE()
        {
            Open();
        }

        protected void Open()
        {
            DoorPanel.Instance.OpenDoor(this);
            GameManager.Instance.CheckDoor(this);
        }

        public void MovePlayer()
        {
            PlayerController.Instance.transform.position = target.position;
        }
        
        public void ResumePlayer()
        {
            PlayerController.Instance.Resume(resume.position);
            thisRoom.EnterRoom();
        }
    }
    
}
