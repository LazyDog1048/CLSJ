using System;
using EquipmentSystem;
using UnityEngine;

namespace data
{
    [Serializable]
    public class BaseSaveData:PackageItemData
    {
        protected BaseSaveData(string name):base(name,new Vector2Int(-1,-1))
        {
            Name = name;
        }
    
    }

    [Serializable]
    public class WeaponData:BaseSaveData
    {
        public int currentAmmo;
        public WeaponData(string name) : base(name)
        {
            
        }
        
        public WeaponData(PackageItemSoData soData) : base(soData.Name)
        {
            currentAmmo = ((GunData)soData).maxAmmo;
            Debug.Log("currentAmmo:"+currentAmmo);
        }
    }
    
    [Serializable]
    public class MeleeWeaponData:BaseSaveData
    {
        public MeleeWeaponData(string name) : base(name)
        {
            
        }
        public MeleeWeaponData(PackageItemSoData soData) : base(soData.Name)
        {
        }
    }
    [Serializable]
    public class AccessoryData:BaseSaveData
    {
        public AccessoryData(string name) : base(name)
        {
            
        }
        public AccessoryData(PackageItemSoData soData) : base(soData.Name)
        {
        }
    }
    
    [Serializable]
    public class SpellData:BaseSaveData
    {
        public SpellData(string name) : base(name)
        {
            
        }
        public SpellData(PackageItemSoData soData) : base(soData.Name)
        {
        }
    }
    
    [Serializable]
    public class PackageData:BaseSaveData
    {
        public PackageData(string name) : base(name)
        {
            
        }
        public PackageData(PackageItemSoData soData) : base(soData.Name)
        {
        }
    }
    
    [Serializable]
    public class CapData:BaseSaveData
    {
        public int reloadRate;
        public int shotStability;
        public int shotCalibration;

        public CapData(string name) : base(name)
        {
            
        }
        public CapData(PackageItemSoData soData) : base(soData.Name)
        {
        }
    }
    
    
    [Serializable]
    public class CoatData:BaseSaveData
    {
        public int pDef;
        public int maxHp;
        
        public CoatData(string name) : base(name)
        {
            
        }
        public CoatData(PackageItemSoData soData) : base(soData.Name)
        {
            
        }
    }
    
    [Serializable]
    public class ShoeData:BaseSaveData
    {
        public int speedRate;
        public int pDef;
        public int maxHp;
        
        public ShoeData(string name) : base(name)
        {
            
        }
        
        public ShoeData(PackageItemSoData soData) : base(soData.Name)
        {
            
        }
    }

    [Serializable]
    public class ShieldData:BaseSaveData
    {
        public int maxShield;
        public int cd;
        public int resume;
        
        public ShieldData(string name) : base(name)
        {
            
        }
        
        public ShieldData(PackageItemSoData soData) : base(soData.Name)
        {
            
        }
    }
    
    [Serializable]
    public class Consumable_Data:BaseSaveData
    {
        public Consumable_Data(string name) : base(name)
        {
            
        }
        
        public Consumable_Data(PackageItemSoData soData) : base(soData.Name)
        {
            
        }
    }
}
