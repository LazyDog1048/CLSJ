using System.Collections;
using System.Collections.Generic;
using data;
using Player;
using UnityEngine;

namespace EquipmentSystem
{
    public class Package : BaseEquipment
    {
        // public List<PackageItemData> packageDataList;
        public List<Consumable_Data> ConsumableDatas;
        public List<Bullet_Data> BulletDatas;
        public Package(PlayerController playerController) : base(playerController)
        {
            ConsumableDatas = LocalPackageThing.GetData().consumableDataList;
            BulletDatas = LocalPackageThing.GetData().bulletDataList;
            // packageDataList = LocalPackageThing.GetData().localPackageDataList;
        }
        
        public override void UpdateEquipment()
        {
            // packageDataList = LocalPackageThing.GetData().localPackageDataList;
        }

        
        public int GetBulletFormPackage(string bulletName,int NeedAmmo)
        {
            int bulletNum = 0;
            for(int i=BulletDatas.Count - 1;i>=0;i--)
            {
                if (BulletDatas[i].Name == bulletName)
                {
                    int count = BulletDatas[i].count;
                    int needCount = NeedAmmo - bulletNum;
                    if (count > needCount)
                    {
                        BulletDatas[i].count -= needCount;
                        bulletNum += needCount;
                    }
                    else
                    {
                        bulletNum += count;
                        BulletDatas.RemoveAt(i);
                    }
                }
                
                if (bulletNum >= NeedAmmo)
                    break;
            }
            return bulletNum;
        }
        
    }
    
}
