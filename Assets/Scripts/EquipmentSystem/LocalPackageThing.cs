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


        public static LocalPackageThing GetData()
        {
            return DataManager.Instance.LocalPackageThing;
        }

        public static void Save()
        {
            // GetDataHandler().SaveData();
        }
    }    
}
