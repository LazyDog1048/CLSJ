using System.Collections;
using System.Collections.Generic;
using data;
using tool;
using UnityEngine;

namespace EquipmentSystem
{
    public class SceneBoxData
    {
        public List<PackageItemData> boxItemDataList;
        
        public SceneBoxData()
        {
            boxItemDataList = new List<PackageItemData>();
        }
        
        public void AddItem(string Name,Vector2Int firstGridPoint,int count)
        {
            // var packageItemSoData = Loader.ResourceLoad<PackageItemSoData>($"So/PackageItemData/{Name}");
            boxItemDataList.Add(new PackageItemData(Name,firstGridPoint,false,count));
        }
    }
    
}
