using System;
using System.Collections.Generic;
using data;
using GridSystem;
using UnityEngine;

namespace EquipmentSystem
{
    [Serializable]
    public class LocalPackageThing :PackageThing
    {

        public LocalPackageThing():base()
        {
            
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
