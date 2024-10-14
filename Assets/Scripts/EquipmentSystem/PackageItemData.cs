using System;
using System.Collections.Generic;
using data;
using UnityEngine;

namespace EquipmentSystem
{
    [Serializable]
    public class PackageItemData
    {
        public string Name;
        public Vector2Int firstGridPoint;
        public bool isRotated;
        public int count;
        
        public PackageItemData(string name,Vector2Int firstGridPoint,bool isRotated = false,int count =1)
        {
            this.Name = name;
            this.firstGridPoint = firstGridPoint;
            this.isRotated = isRotated;
            this.count = count;
        }
    }

}
