using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GridSystem
{
    public class PlayerWeaponCell : PlayerEquipmentCell
    {
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if(PackageItemPreview.Instance.currentUiPackageItem is WeaponPackageItem weaponPackageItem)
            {
                // weaponPackageItem.PutOnGrid(this);
            }             
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
    
}
