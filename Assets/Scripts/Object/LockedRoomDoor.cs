using System.Collections.Generic;
using data;
using EquipmentSystem;
using ui;
using UnityEngine;

namespace game
{
    public class LockedRoomDoor : RoomDoor
    {
        [SerializeField]
        private List<PackageItemSoData> lockItemDatas;
        
        [SerializeField]
        private string LockedDescription = "Door is locked!";
        [SerializeField]
        private string UnlockDescription = "锁开了";
        
        private bool isOpened = false;
        
        public override void PressE()
        {
            if(isOpened)
                Open();
            else if (CheckKey())
            {
                UnlockDoor();
                GameManager.Instance.CheckDoor(this);
                DescriptionUi.Instance.ShowDescription(UnlockDescription);
            }
            else
            {
                DescriptionUi.Instance.ShowDescription(LockedDescription);
            }
        }


        private void UnlockDoor()
        {
            var list = LocalPackageThing.GetData().consumableDataList;
            for(int i=list.Count-1;i>=0;i--)
            {
                foreach (var key in lockItemDatas)
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

        private bool CheckKey()
        {
            int much = 0;
            foreach (var consumableData in LocalPackageThing.GetData().consumableDataList)
            {
                foreach (var key in lockItemDatas)
                {
                    if (key.Name.Equals(consumableData.Name))
                    {
                        much++;
                    }
                }
                
            }
            return  much == lockItemDatas.Count;
        }
        
    }
    
}
