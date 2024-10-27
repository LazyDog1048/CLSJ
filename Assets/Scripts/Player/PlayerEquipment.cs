using System.Collections;
using System.Collections.Generic;
using data;
using EquipmentSystem;
using GridSystem;
using UnityEngine;

namespace Player
{
    public class PlayerEquipment
    {
        public int EquipmentHp =>coat.Hp + leftShoe.Hp + rightShoe.Hp;
        public int EquipmentPDef =>coat.PDef + leftShoe.PDef + rightShoe.PDef;

        public float shotDelay => currentWeapon.gunParameter.shotDelay.FinalValue;
        public float bulletSpeed => currentWeapon.gunParameter.bulletSpeed.FinalValue;
        
        
        public float maxShotShake => currentWeapon.gunParameter.maxShotShake.FinalValue;
        public float minShotShake => currentWeapon.gunParameter.minShotShake.FinalValue;
        
        public float minCalibration => minShotShake - currentWeapon.gunParameter.minCalibration;
        public float shotStability => currentWeapon.gunParameter.shotStability.FinalValue + cap.shotStability;
        public float shotCalibration => currentWeapon.gunParameter.shotCalibration.FinalValue + cap.shotCalibration;
        public int maxAmmo => currentWeapon.gunParameter.maxAmmo.FinalValue;


        #region PlayerMoveSpeed
        public float playerMoveSpeed =>  playerMoveSpeedValue * playerMoveSpeedRate;
        public float playerMoveSpeedRate => (100  + FinalSpeedRate)/100f;
        public int FinalSpeedRate => EquipmentSpeedRate + playerController.playerMove.SpeedRate;
        public int EquipmentSpeedRate =>leftShoe.SpeedRate + rightShoe.SpeedRate;
        public float playerMoveSpeedValue => playerController.playerMove.BaseSpeed;
        #endregion

        #region PlayerReloadTime
        public float reloadTime =>  reloadTimeValue * reloadTimeRate;
        public float reloadTimeRate => (100  + cap.reloadRate)/100f;
        public float reloadTimeValue => currentWeapon.gunParameter.reloadTime.FinalValue;
        #endregion

        public bool firstGun { get;private set; }
        public PlayerGun currentWeapon => firstGun?weapon_1:lamp;
        // public BaseGun currentWeapon => weapon_1;
        public PlayerGun weapon_1 { get;private set; }
        public PlayerGun tempWeapon { get;private set; }
        public PlayerGun lamp { get;private set; }
        public Lamp tempLamp { get;private set; }
        public MeleeWeapon meleeWeapon{ get;private set; }
        public Armor_Coat coat { get;private set; }
        public Armor_LeftShoe leftShoe { get;private set; }
        public Armor_RightShoe rightShoe { get;private set; }
        public Armor_Cap cap { get;private set; }
        public Spell spell { get;private set; }
        public Armor_Shield shield { get;private set; }
        public Accessory accessory { get;private set; }
        public Package package { get;private set; }
        public Consumable consumable_1 { get;private set; }
        public Consumable consumable_2 { get;private set; }
        public Consumable consumable_3 { get;private set; }
        public Consumable consumable_4 { get;private set; }

        PlayerController playerController;
        public PlayerEquipment(PlayerController playerController)
        {
            this.playerController = playerController;
            weapon_1 = PlayerController.Instance.ChangeGun_1();
            lamp = PlayerController.Instance.ChangeGun_2();
            if (lamp is Lamp l)
            {
                tempLamp = l;
            }
            
            firstGun = true;


            meleeWeapon = new MeleeWeapon(playerController);
            coat = new Armor_Coat(playerController);
            leftShoe = new Armor_LeftShoe(playerController);
            rightShoe = new Armor_RightShoe(playerController);
            cap = new Armor_Cap(playerController);
            spell = new Spell(playerController);
            shield = new Armor_Shield(playerController);
            accessory = new Accessory(playerController);
            package = new Package(playerController);
            consumable_1 = new Consumable(playerController,1);
            consumable_2 = new Consumable(playerController,2);
            consumable_3 = new Consumable(playerController,3);
            consumable_4 = new Consumable(playerController,4);
            
            playerController.DelayRealTimeExecute(0.1f, () =>
            {
                playerController.UpdateGun(currentWeapon);
            });
        }

        public void UpdateWeapon()
        {
            weapon_1 = PlayerController.Instance.ChangeGun_1();
            playerController.DelayRealTimeExecute(0.1f, () =>
            {
                if (weapon_1 is not Hand)
                {
                    tempWeapon = weapon_1;
                }
                Package_Panel.Instance.playerPackageUiGridSystem.CheckEquipIcon();
                playerController.UpdateGun(currentWeapon);
            });
        }
        
        public void UpdateLamp()
        {
            lamp = PlayerController.Instance.ChangeGun_2();
            playerController.DelayRealTimeExecute(0.1f, () =>
            {
                if (lamp is Lamp l && currentWeapon == lamp)
                {
                    tempLamp = l;
                    tempLamp.LightOn();
                }
                else if(tempLamp != null)
                {
                    tempLamp.LightOff();
                }
                
                Package_Panel.Instance.playerPackageUiGridSystem.CheckEquipIcon();
                playerController.UpdateGun(currentWeapon);
            });
        }

        public void UpgradeLamp()
        {
            lamp = PlayerController.Instance.ChangeGun_2();
            tempLamp = lamp as Lamp;
            playerController.DelayRealTimeExecute(0.1f, () =>
            {
                if (lamp is Lamp l && currentWeapon == lamp)
                {
                    tempLamp = l;
                    tempLamp.LightOn();
                }
                else if(tempLamp != null)
                {
                    tempLamp.LightOff();
                }
                
                Package_Panel.Instance.playerPackageUiGridSystem.CheckEquipIcon();
                playerController.UpdateGun(currentWeapon);
            });
        }
        
        public void SwitchToWeapon1()
        {
            firstGun = true;
            playerController.UpdateGun(weapon_1);
            if(lamp is Lamp l)
            {
                l.LightOff();
            }
        }
        
        public void SwitchToWeapon2()
        {
            firstGun = false;
            playerController.UpdateGun(lamp);
            if(lamp is Lamp l)
            {
                l.LightOn();
            }
        }
        
        public void EquipWeapon()
        {
            GunData gunData = weapon_1.gunData;
            if (gunData == null)
                return;
            if (weapon_1 is Hand)
            {
                LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
                localPlayerDataThing.weapon_1 = tempWeapon.WeaponData;
                LocalPlayerDataThing.Save();
                PlayerController.Instance.playerEquipment.UpdateWeapon();
            }
            else
            {
                LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
                localPlayerDataThing.weapon_1 = new WeaponData("Hand");
            }
            LocalPlayerDataThing.Save();
            UpdateWeapon();
        }
        
        public void EquipLamp()
        {
            GunData gunData = lamp.gunData;
            if (gunData == null)
                return;
            if (lamp is Hand)
            {
                LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
                localPlayerDataThing.weapon_2 = tempLamp.WeaponData;
            }
            else
            {
                LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
                localPlayerDataThing.weapon_2 = new WeaponData("Hand");
            }
            LocalPlayerDataThing.Save();
            UpdateLamp();
        }
        
        
                
        public void UpdateEquipment()
        {
            weapon_1 = PlayerController.Instance.ChangeGun_1();
            // weapon_2 = PlayerController.Instance.ChangeGun_2();
            //
            // if(weapon_1 != null && weapon_2 == null)
            //     firstGun = true;
            // if(weapon_1 == null && weapon_2 != null)
            //     firstGun = false;
            
            meleeWeapon.UpdateEquipment();
            coat.UpdateEquipment();
            leftShoe.UpdateEquipment();
            rightShoe.UpdateEquipment();
            cap.UpdateEquipment();
            spell.UpdateEquipment();
            shield.UpdateEquipment();
            accessory.UpdateEquipment();
            package.UpdateEquipment();
            consumable_1.UpdateEquipment();
            consumable_2.UpdateEquipment();
            consumable_3.UpdateEquipment();
            consumable_4.UpdateEquipment();
            
            
            playerController.DelayRealTimeExecute(0.1f, () =>
            {
                PlayerUiPanel.Instance.SwitchGun(currentWeapon);
            });
        }

    }    
}
