using System;
using System.Collections.Generic;
using data;
using EquipmentSystem;
using GridSystem;
using UnityEngine;


namespace game
{
    [Serializable]
    public class BoxBullet
    {
        public BulletData bullet;
        public int count =10;
    }
    public class Container : MonoBehaviour
    {
        private PackageUiGridSystem boxUiGridSystem;
        public List<UiPackageItem> boxItemList =>boxUiGridSystem.boxItemDataList;
        
        public List<PackageItemSoData> itemDatas;
        public List<BoxBullet> bulletDatas;

        
        private PackageThing packageThing;
        private bool isOpen = false;
        private bool isFirstOpen = true;

        private void Awake()
        {
            packageThing = new PackageThing();
        }

        public void OpenOrClose()
        {
            boxUiGridSystem = Package_Panel.Instance.boxUiGridSystem;
            if (isOpen)
            {
                Close();
                isOpen = false;
                Package_Panel.Instance.Hide();
            }
            else
            {
                Package_Panel.Instance.Show();
                if(isFirstOpen)
                    FirstOpen();
                else
                {
                    boxUiGridSystem.LoadPackage(packageThing);
                }
                isOpen = true;
            }
        }

        private void FirstOpen()
        {
            isFirstOpen = false;   
            foreach (var item in itemDatas)
            {
                AddBox(item.Name);
            }
            foreach (var bullet in bulletDatas)
            {
                AddBox(bullet.bullet.Name,bullet.count);
            }
        }

        private void Close()
        {
            ClosePanelSaveData();
        }


        public void OpenPanelSaveData()
        {
            List<UiPackageItem> tPackageItems = new List<UiPackageItem>();
            foreach (var cell in boxUiGridSystem.Grid.GridArray)
            {
                if(cell.UiPackageItem != null && !tPackageItems.Contains(cell.UiPackageItem))
                    tPackageItems.Add(cell.UiPackageItem);
            }

            packageThing.ClearPackageData();
            foreach (var packageItem in tPackageItems)
            {
                packageItem.SaveItemToPackage();
            }
        }
        public void ClosePanelSaveData()
        {
            packageThing.ClearPackageData();
            
            List<UiPackageItem> tPackageItems = new List<UiPackageItem>();
            foreach (var cell in boxUiGridSystem.Grid.GridArray)
            {
                if(cell.UiPackageItem != null && !tPackageItems.Contains(cell.UiPackageItem))
                    tPackageItems.Add(cell.UiPackageItem);
            }
            foreach (var packageItem in tPackageItems)
            {
                packageThing.AddPackageData(packageItem);
            }
        }

        
        public void AddBox(string itemName)
        {
            var packageItemSoData = ResourcesDataManager.GetPackageItemSoData(itemName);
            var shapeData = packageItemSoData.shapeData;
            Vector2Int enablePoint = boxUiGridSystem.FindEnablePoint(shapeData);
            if (enablePoint.x == -1)
                return;
            PackageItemData packageItemData = PackageItemData.CreatPackageItemData(packageItemSoData);
            packageItemData.firstGridPoint = enablePoint;
            boxUiGridSystem.AddItem(packageItemData);            
        }

        public void AddBox(string itemName,int count)
        {
            foreach(var item in boxItemList)
            {
                if (item.packageItemData.Name == itemName && item.packageItemSoData.ItemType == PackageItemType.Bullet)
                {
                    item.Count += count;
                    return;
                }
            }
            var packageItemSoData = ResourcesDataManager.GetPackageItemSoData(itemName);
            var shapeData = packageItemSoData.shapeData;
            Vector2Int enablePoint = boxUiGridSystem.FindEnablePoint(shapeData);
            if (enablePoint.x == -1)
                return;
            PackageItemData packageItemData = PackageItemData.CreatPackageItemData(packageItemSoData);
            packageItemData.firstGridPoint = enablePoint;
            packageItemData.count = count;
            boxUiGridSystem.AddItem(packageItemData);            
        }
    }
    
}
