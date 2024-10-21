using System;
using System.Collections.Generic;
using other;
using tool;
using UnityEngine;

namespace data
{
    public class ResourcesDataManager : Class_Singleton<ResourcesDataManager>
    {
        #region path

        private static string PackageItemSoPath = $"So/PackageItemData";
        private static string PrefabPath = $"So/Prefab";
        #endregion
        
        #region Data
        private Dictionary<string,PackageItemSoData> packageItemSoDic = new Dictionary<string, PackageItemSoData>();
        private Dictionary<string,GameObject> prefabDic = new Dictionary<string, GameObject>();
        
        #endregion

        #region InitData
        protected override void Init()
        {
            base.Init();
            InitPackageItemSoDic();
        }
        
        public override void GameQuite()
        {
            packageItemSoDic.Clear();
        }
        
        private void InitPackageItemSoDic()
        {
            var arr = Loader.GetAllForm_Resource<PackageItemSoData>(PackageItemSoPath);
            foreach (var baseData in arr)
            {
                packageItemSoDic.CheckAdd(baseData.Name,baseData);
            }
        }
        //
        // private void InitPrefabDic()
        // {
        //     var arr = Loader.GetAllForm_Resource<GameObject>(PackageItemSoPath);
        //     foreach (var baseData in arr)
        //     {
        //         packageItemSoDic.CheckAdd(baseData.Name,baseData);
        //     }
        // }
        //
        #endregion

        #region GetData

        public static PackageItemSoData GetPackageItemSoData(string name)
        {
            if(!Instance.packageItemSoDic.ContainsKey(name))
                throw new NullReferenceException($"字典中未找到该 PackageItemSoData name {name}");

            return Instance.packageItemSoDic[name];
        }

        #endregion
    }
}
