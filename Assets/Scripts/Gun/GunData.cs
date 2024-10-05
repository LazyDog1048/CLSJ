using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
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
    public class GunData : ScriptableObject
    {
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
        public ShotType shotType;
        public ShotMode shotMode;
        public float shotDelay;
        
        public float maxShotShake;
        public float minShotShake;
        public float shotStability;
        public float shotCalibration;
        
        public float reloadTime;
        public int maxAmmo;
        public int penetrateNum;
        public float bulletSpeed;
        public float bulletStayTime;
        public BulletData bulletData;
        
        public GunParameter(GunData data)
        {
            shotType = data.shotType;
            shotMode = data.shotMode;
            shotDelay = data.shotDelay;
            maxShotShake = data.maxShotShake;
            minShotShake = data.minShotShake;
            shotStability = data.shotStability;
            shotCalibration = data.shotCalibration;
            reloadTime = data.reloadTime;
            maxAmmo = data.maxAmmo;
            penetrateNum = data.penetrateNum;
            bulletSpeed = data.bulletSpeed;
            bulletStayTime = data.bulletStayTime;
            bulletData = data.bulletData;
        }
    }
    
}
