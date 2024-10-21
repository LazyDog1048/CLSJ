using buff;
using data;
using EquipmentSystem;
using game;
using UnityEngine;

namespace EquipmentSystem
{
    public enum ShotType
    {
        Single,
        Triple,
        ShotGun
    }
    
    public enum ShotMode
    {
        Click,
        Press
    }
    
    [CreateAssetMenu(fileName = "GunData", menuName = "Data/GunData")]
    public class GunData : WeaponSoData
    {
        public int Damage;
        public ShotType shotType = ShotType.Single;
        public ShotMode shotMode = ShotMode.Click;
        public float shotDelay = 0.5f;
        
        public float maxShotShake = 50;
        public float minShotShake = 10;
        public float shotStability = 4;
        public float shotCalibration = 10;
        
        public float reloadTime = 1.0f;
        public int maxAmmo = 10;
        public int penetrateNum = 1;
        public float bulletSpeed = 10.0f;
        public float bulletStayTime = 4.0f;
        public BulletData bulletData;
    }
    
    public class GunParameter
    {
        public Animator gunAnimator;
        public ShotType shotType;
        public ShotMode shotMode;

        public int penetrateNum;
        public float bulletStayTime;
        
        public EventExtraFloat shotDelay{ get; set; }
        public EventExtraFloat bulletSpeed{ get; set; }
        public EventExtraFloat reloadTime{ get; set; }
        
        public int reloadTimeRate{ get; set; }
        public float reloadTimeExtraValue{ get; set; }
        public EventExtraFloat maxShotShake{ get; set; }
        public EventExtraFloat minShotShake{ get; set; }
        public EventExtraFloat shotStability{ get; set; }
        public EventExtraFloat shotCalibration{ get; set; }
        public EventExtraInt maxAmmo{ get; set; }
        
        public BulletData bulletData;
        
        public GunParameter(GunData data)
        {
            shotType = data.shotType;
            shotMode = data.shotMode;
            shotDelay = new EventExtraFloat(data.shotDelay);
            maxShotShake = new EventExtraFloat(data.maxShotShake);
            minShotShake = new EventExtraFloat(data.minShotShake);
            shotStability = new EventExtraFloat(data.shotStability);
            shotCalibration = new EventExtraFloat(data.shotCalibration);
            reloadTime = new EventExtraFloat(data.reloadTime);
            
            maxAmmo = new EventExtraInt(data.maxAmmo);
            bulletSpeed = new EventExtraFloat(data.bulletSpeed);

            
            penetrateNum = data.penetrateNum;
            bulletStayTime = data.bulletStayTime;
            bulletData = data.bulletData;
            
        }
    }
    
}
