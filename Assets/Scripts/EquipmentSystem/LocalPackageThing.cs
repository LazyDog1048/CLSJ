using System;
using System.Collections;
using System.Collections.Generic;
using data;
using UnityEngine;

namespace EquipmentSystem
{
    [Serializable]
    public class LocalPackageThing
    {
        public List<PackageItemData> localPackageDataList;
        
        public LocalPackageThing()
        {
            localPackageDataList = new List<PackageItemData>();
        }
        
        public void AddPackageData(string Name,Vector2Int firstGridPoint,bool isRotate)
        {
            localPackageDataList.Add(new PackageItemData(Name,firstGridPoint,isRotate));
            Save();
        }
        public void AddPackageData(PackageItemData data)
        {
            localPackageDataList.Add(data);
            Save();
        }

        public void RemovePackageData(Vector2Int pos)
        {
            for (int i = 0; i < localPackageDataList.Count; i++)
            {
                if (localPackageDataList[i].firstGridPoint == pos)
                {
                    localPackageDataList.RemoveAt(i);
                    break;
                }
            }
            Save();
        }

        public bool CheckHadPackageData(Vector2Int position)
        {
            return GetPackageData(position) != null;
        }
        
        public PackageItemData GetPackageData(Vector2Int position)
        {
            foreach (var packageData in localPackageDataList)
            {
                if (packageData.firstGridPoint == position)
                {
                    return  packageData;
                }
            }

            return null;
        }
        
       
        
        public static KeepDataHandler<LocalPackageThing> GetDataHandler()
        {
            return DataManager.Instance.LocalPackageThingHandler;
        }
        
        public static LocalPackageThing GetData()
        {
            return DataManager.Instance.LocalPackageThingHandler.Data;
        }

        public static void Save()
        {
            GetDataHandler().SaveData();
        }
    }    
}
