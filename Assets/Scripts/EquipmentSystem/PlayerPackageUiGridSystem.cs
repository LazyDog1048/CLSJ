using System;
using System.Collections.Generic;
using data;
using EquipmentSystem;
using UnityEngine;

namespace GridSystem
{
    public class PlayerPackageUiGridSystem : PackageUiGridSystem
    {

        public void OpenPanelSaveData()
        {
            List<UiPackageItem> tPackageItems = new List<UiPackageItem>();
            foreach (var cell in Grid.GridArray)
            {
                if(cell.UiPackageItem != null && !tPackageItems.Contains(cell.UiPackageItem))
                    tPackageItems.Add(cell.UiPackageItem);
            }

            LocalPackageThing.GetData().ClearPackageData();
            foreach (var packageItem in tPackageItems)
            {
                packageItem.SaveItemToPackage();
            }
            LocalPackageThing.Save();
        }
        public void ClosePanelSaveData()
        {
            List<UiPackageItem> tPackageItems = new List<UiPackageItem>();
            foreach (var cell in Grid.GridArray)
            {
                if(cell.UiPackageItem != null && !tPackageItems.Contains(cell.UiPackageItem))
                    tPackageItems.Add(cell.UiPackageItem);
            }

            LocalPackageThing.GetData().ClearPackageData();
            foreach (var packageItem in tPackageItems)
            {
                packageItem.SaveItemToPackage();
            }
            LocalPackageThing.Save();
        }

        public void CheckEquipIcon()
        {
            foreach (var item in boxItemDataList)
            {
                item.CheckEquip();
            }
        }
    }
    
}
