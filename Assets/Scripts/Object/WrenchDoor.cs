using EquipmentSystem;
using ui;
using UnityEngine;

namespace game
{
    public class WrenchDoor : LockedRoomDoor
    {
        [SerializeField]
        private bool lockSide;
        
        public override void PressE()
        {
            if(isOpened)
                Open();
            else if (!lockSide && CheckKey())
            {
                UnlockDoor();
                Open();
                DescriptionUi.Instance.ShowDescription(UnlockDescription);
            }
            else
            {
                DescriptionUi.Instance.ShowDescription(LockedDescription);
            }
        }
        
        protected override void UnlockDoor()
        {
            var list = LocalPackageThing.GetData().consumableDataList;
            for(int i=list.Count-1;i>=0;i--)
            {
                foreach (var key in lockItemDatas)
                {
                    if (key.Name.Equals(list[i].Name))
                    {
                        break;
                    }
                }
            }
            isOpened = true;
            otherSide.isOpened = true;
        }
    }
    
}
