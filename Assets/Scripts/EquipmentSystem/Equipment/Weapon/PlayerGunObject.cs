using EquipmentSystem;
using game;
using GridSystem;
using item;
using other;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Player
{
    public class PlayerGunObject : GunObject
    {
        public void ReloadGun(PlayerGun playerGun)
        {
            ReloadGun(playerGun.gunData);
            PlayerUiPanel.Instance.SwitchGun(playerGun);
            PlayerController.Instance.playerFlashlight.SwitchGun(playerGun);
        }
    }
    
}
