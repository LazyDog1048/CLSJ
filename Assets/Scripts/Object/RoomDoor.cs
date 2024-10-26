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
        private List<PackageItemSoData> itemDatas;
        private bool isOpened = false;

        private Transform target;
        protected override void Awake()
        {
            base.Awake();
            target = transform.Find("Target");
        }


        public override void PressE()
        {
            if(isOpened)
                Open();
            else if (CheckKey())
            {
                FirstOpen();
            }
            else
            {
                Debug.Log("Door is locked!");
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

        private void FirstOpen()
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
            Open();
        }

        private void Open()
        {
            DoorPanel.Instance.OpenDoor(this);
        }

        public void MovePlayer()
        {
            PlayerController.Instance.transform.position = target.position;
        }
    }
    
}
