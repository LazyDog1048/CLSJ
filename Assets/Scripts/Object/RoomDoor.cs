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

        private Transform Echeck;
        public Vector3 TargetPosition => target.position;
        public Vector3 ResumePosition => resume.position;
        // private List<GameObject> childs;

        protected override void Awake()
        {
            base.Awake();
            target = transform.Find("Target");
            resume = transform.Find("Resume");
            thisRoom = transform.parent.parent.GetComponent<Room>();
            
            Echeck = transform.Find("ECheck");

            if (transform.localScale.x < 0)
            {
                Echeck.localScale = new Vector3(-1, 1, 1);
            }
            // childs = new List<GameObject>();
            // for (int i = 0; i < transform.childCount; i++)
            // {
            //     childs.Add(transform.GetChild(i).gameObject);
            // }
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
            GameManager.Instance.EnterTargetRoom(this);
            // PlayerController.Instance.transform.position = target.position;
        }
    }
    
}
