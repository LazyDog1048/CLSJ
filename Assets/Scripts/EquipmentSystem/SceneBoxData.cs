using System.Collections.Generic;
using data;
using GridSystem;
using other;
using UnityEngine;

namespace EquipmentSystem
{
    public class SceneBox:Class_Singleton<SceneBox>
    {
        public List<UiPackageItem> boxItemList =>boxUiGridSystem.boxItemDataList;
        PackageUiGridSystem boxUiGridSystem;


        protected override void Init()
        {
            base.Init();
            boxUiGridSystem = Package_Panel.Instance.boxUiGridSystem;
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
                if (item.packageItemData.Name == itemName && item.packageItemSoData.ItemType == PackageItemType.Consumable)
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
        
        public void AddPreview(string itemName,int count)
        {
            var packageItemSoData = ResourcesDataManager.GetPackageItemSoData(itemName);
            PackageItemData packageItemData = PackageItemData.CreatPackageItemData(packageItemSoData);
            packageItemData.firstGridPoint = new Vector2Int(-1,-1);
            packageItemData.count = count;
            Package_Panel.Instance.AddPreviewItem(packageItemData);
        }
        
        public void TestBox()
        {
            AddBox("Gun");
            AddBox("ShotGun");
            AddBox("MeleeWeapon");
            AddBox("Armor_Cap");
            AddBox("Armor_Coat");
            AddBox("Armor_LeftShoe");
            AddBox("Armor_RightShoe");
            AddBox("Spell");
            AddBox("Shield");
            AddBox("Accessory");
            AddBox("Consumable_1");
            AddBox("Package");
            AddBox("Magic_Bullet",100);
            
        }
    }
    
}
