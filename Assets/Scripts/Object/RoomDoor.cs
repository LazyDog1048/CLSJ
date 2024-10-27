using System;
using System.Collections.Generic;
using data;
using EquipmentSystem;
using NUnit.Framework;
using Player;
using ui;
using UnityEngine;

namespace game
{
    public class RoomDoor : SceneObject
    {
        [SerializeField]
        public Room targetRoom;
        [SerializeField]
        public Room thisRoom;
        
        [SerializeField]
        private List<PackageItemSoData> itemDatas;
        [SerializeField]
        private string Description = "Open the door";
        
        private bool isOpened = false;

        [SerializeField]
        private Transform target;
        [SerializeField]
        private Transform resume;
        
        


        public override void PressE()
        {
            if(isOpened)
                Open();
            else if (CheckKey())
            {
                UnlockDoor();
                GameManager.Instance.CheckDoor(this);
                DescriptionUi.Instance.ShowDescription("Door is open!");
            }
            else
            {
                DescriptionUi.Instance.ShowDescription("Door is locked!");
            }
        }

        private bool CheckKey()
        {
            int much = 0;
            foreach (var consumableData in LocalPackageThing.GetData().consumableDataList)
            {
                foreach (var key in itemDatas)
                {
                    if (key.Name.Equals(consumableData.Name))
                    {
                        much++;
                    }
                }
                
            }
            return  much == itemDatas.Count;
        }

        private void UnlockDoor()
        {
            var list = LocalPackageThing.GetData().consumableDataList;
            for(int i=list.Count-1;i>=0;i--)
            {
                foreach (var key in itemDatas)
                {
                    if (key.Name.Equals(list[i].Name))
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }
            }
            LocalPackageThing.Save();
            isOpened = true;
        }

        private void Open()
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
