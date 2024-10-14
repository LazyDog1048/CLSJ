using System;
using System.Collections.Generic;
using data;
using EquipmentSystem;
using item;
using other;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GridSystem
{
    public class PackageItemPreview : Mono_Singleton<PackageItemPreview>
    {
        public IPlayerPackageSlot playerPackageSlot;
        // [SerializeField]
        // private UiPackageItem uiPackageItem;
        public UiPackageItem currentUiPackageItem { get; private set; }
        private bool CanPutDown => playerPackageSlot != null && currentUiPackageItem != null;
        private bool CanPickUp => playerPackageSlot != null && currentUiPackageItem == null;

        private bool leftPress;
        private bool done;

        public void SwitchGrid(IPlayerPackageSlot gridSystem)
        {
            playerPackageSlot = gridSystem;
        }

        
        public void ItemUpdatePosition()
        {
            if(currentUiPackageItem != null)
            {
                currentUiPackageItem.UpdatePosition();
            }
        }
        
        public void LeftClick(InputAction.CallbackContext context)
        {
            if(context.phase== InputActionPhase.Started)
            {
                if (CanPickUp)
                {
                    PickUpItem();
                    done = true;
                }
                leftPress = true;
            }
            else if(context.phase == InputActionPhase.Canceled)
            {
                if (!done && CanPutDown)
                {
                    PutDownItem();
                }
                leftPress = false;
                done = false;
            }
        }
        
        public void RotateItem(InputAction.CallbackContext context)
        {
            if (leftPress && context.phase == InputActionPhase.Started)
            {
                if(currentUiPackageItem != null)
                {
                    currentUiPackageItem.CheckRotate();
                }
            }
        }


        private void PutDownItem()
        {
            playerPackageSlot.PutDownItem(currentUiPackageItem);
            
        }

        private void PickUpItem()
        {
            playerPackageSlot.PickUpItem();
        }
        
        public void SetPackageItem(UiPackageItem item)
        {
            currentUiPackageItem = item;
            currentUiPackageItem.transform.SetParent(transform);
        }
        public void ClearItem()
        {
            currentUiPackageItem = null;
        }
    }
    
}
