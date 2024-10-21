using System.Collections.Generic;
using EquipmentSystem;
using other;
using UnityEngine;

namespace data
{
   public class DataManager : Class_Singleton<DataManager>
    {
        public static string SavePath
        {
            get
            {
#if UNITY_EDITOR
                return $"{Application.dataPath}/LocalData";
#else
                return $"{Application.persistentDataPath}/LocalData";
#endif
            }
        }

        
        #region saveData

        public KeepDataHandler<LocalPackageThing> LocalPackageThingHandler => LocalPackageThingHandlerList[FileIndex];
        public KeepDataHandler<LocalPlayerDataThing> LocalPlayerDataThingHandler => LocalPlayerDataThingHandlerList[FileIndex];
        
        
        public List<KeepDataHandler<LocalPackageThing>> LocalPackageThingHandlerList;
        public List<KeepDataHandler<LocalPlayerDataThing>> LocalPlayerDataThingHandlerList;
        
        
        #endregion
        
        #region Data

        
        public static int FileIndex { get;private set; }
        
        #endregion

        public void SetFileIndex(int index)
        {
            FileIndex = index;
        }
        
        public static string GetLoadPath(int index)
        {
            return index < 0 ? $"{SavePath}/Common" : $"{SavePath}/File_{index}";
        }
        
        protected override void Init()
        {
            base.Init();
            LocalPackageThingHandlerList = new List<KeepDataHandler<LocalPackageThing>>();
            LocalPlayerDataThingHandlerList = new List<KeepDataHandler<LocalPlayerDataThing>>();
            StartLoad(0);
            StartLoad(1);
            StartLoad(2);
        }

        public void StartLoad(int index)
        {
            
            LocalPackageThingHandlerList.Add(new KeepDataHandler<LocalPackageThing>(index));
            LocalPlayerDataThingHandlerList.Add(new KeepDataHandler<LocalPlayerDataThing>(index));
        }

        public override void GameQuite()
        {
            SaveAllData();
        }

        #region data

        public void SaveAllData()
        {
            LocalPackageThingHandlerList[0].SaveData();
            LocalPlayerDataThingHandlerList[0].SaveData();
        }

        public void ClearData(int index)
        {
            // foreach (var keeper in dataKeeperDic[index])
            // {
            //     keeper.SetToDefaultData();
            // }
        }

        #endregion
    }
    
}