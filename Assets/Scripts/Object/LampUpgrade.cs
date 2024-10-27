using System.Collections.Generic;
using data;
using EquipmentSystem;
using GridSystem;
using Player;
using ui;
using UnityEngine;

namespace game
{
    public class LampUpgrade : SceneObject
    {
        [SerializeField]
        private GunData lamp;
        [SerializeField]
        private GunData strongLamp;
        private PackageUiGridSystem playerPackageUiGridSystem;
        public List<UiPackageItem> boxItemList =>playerPackageUiGridSystem.boxItemDataList;
        
        
        public override void PressE()
        {
        
            playerPackageUiGridSystem = Package_Panel.Instance.playerPackageUiGridSystem;
            if (!CheckPackage())
            {
                DescriptionUi.Instance.ShowDescription("背包没有灯笼");
            }
            else
            {
                DescriptionUi.Instance.ShowDescription("灯笼已升级");
                UpgradeLamp();
            }
        }

        private bool CheckPackage()
        {
            foreach (var item in LocalPackageThing.GetData().weaponDataList)
            {
                //had lamp
                if(item.Name.Equals(lamp.Name))
                {
                    return true;
                }
            }
            return false;
        }

        private void UpgradeLamp()
        {
            var list = LocalPackageThing.GetData().weaponDataList;
            foreach (var data in list)
            {
                if (data.Name.Equals(lamp.Name))
                {
                    data.Name = strongLamp.Name;
                }
            }
            
            // LocalPackageThing.GetData().weaponDataList.Add(new WeaponData(strongLamp.Name));
            // playerPackageUiGridSystem.RemoveItem(lamp.Name);
            // playerPackageUiGridSystem.PutDownItem(strongLamp.Name);
            
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            localPlayerDataThing.weapon_2 = new WeaponData(strongLamp.Name);
            PlayerController.Instance.playerEquipment.UpgradeLamp();
            
            LocalPlayerDataThing.Save();
        }
    }
    
}
