using System.Collections.Generic;
using EquipmentSystem;
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
        
        private Image followBg;
        public UiPackageItem currentUiPackageItem { get; private set; }
        private bool CanPutDown => playerPackageSlot != null && currentUiPackageItem != null;
        private bool CanPickUp => playerPackageSlot != null && currentUiPackageItem == null;

        public bool OnPickUp => currentUiPackageItem != null;
        
        private bool leftPress;
        private bool done;

        private static Color canPutColor;
        private static Color cantPutColor;
        RectTransform rectTransform => transform as RectTransform;
        protected override void Awake()
        {
            base.Awake();
            canPutColor = ColorTool.Trans16StrToColor("#745748");
            cantPutColor = ColorTool.Trans16StrToColor("#5a1106");
            cantPutColor.a = 0.6f;
            followBg = transform.Find("FollowItem").GetComponent<Image>();
            followBg.gameObject.SetActive(false);
        }

        public void EnterUiPackageItem(UiPackageItem item)
        {
            if(OnPickUp)
                return;
            transform.position = item.transform.position;
            rectTransform.sizeDelta = item.rectTransform.sizeDelta;
        }
        
        public void SwitchSlot(IPlayerPackageSlot gridSystem)
        {
            playerPackageSlot = gridSystem;
            if (playerPackageSlot is PackageUiGridSystem)
            {
                ItemUpdatePosition();
                if(currentUiPackageItem != null)
                    followBg.gameObject.SetActive(true);
            }
            
        }
        public void RemoveSlot()
        {
            playerPackageSlot = null;
            followBg.gameObject.SetActive(false);
        }
        
        public void SetPackageItem(UiPackageItem item)
        {
            currentUiPackageItem = item;
            currentUiPackageItem.transform.SetParent(transform);


            if (playerPackageSlot is PackageUiGridSystem)
            {
                ItemUpdatePosition();
                followBg.rectTransform.sizeDelta = item.rectTransform.sizeDelta - new Vector2(0.5f,0.5f);
                followBg.color = canPutColor;
                
                if(currentUiPackageItem != null)
                    followBg.gameObject.SetActive(true);
            }
        }
        public void ClearItem()
        {
            currentUiPackageItem = null;
            followBg.gameObject.SetActive(false);
        }
        
        public void ItemUpdatePosition()
        {
            if(currentUiPackageItem != null)
            {
                currentUiPackageItem.UpdatePosition();
                UpdateItemEnable();
            }
        }
        
        private void UpdateItemEnable()
        {
            if(playerPackageSlot is not PackageUiGridSystem uiGridSystem)
                return;
            bool canPut = true;
            var firstCell = currentUiPackageItem.Cells[0];
            foreach (var cell in currentUiPackageItem.Cells)
            {
                UiGridObject playerGrid = uiGridSystem.Grid.GetGridObject(cell.transform.position);
                if (playerGrid == null || !playerGrid.CanBuild())
                {
                    canPut = false;
                    break;
                }
            }
            UiGridObject first = uiGridSystem.Grid.GetGridObject(firstCell.transform.position);
            if (first == null)
            {
                followBg.transform.position =currentUiPackageItem.transform.position;
            }
            else
            {
                Vector3 offset = first.gridCellTransform.position - firstCell.transform.position;
                followBg.transform.position =currentUiPackageItem.transform.position + offset;
                followBg.color = canPut ? canPutColor : cantPutColor;
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
        
        public void RightClick(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                if (leftPress)
                {
                    if(currentUiPackageItem != null)
                    {
                        currentUiPackageItem.CheckRotate();
                    }
                }
                else if(!OnPickUp)
                {
                   if(UiPackageItem.cursorUiPackageItem != null)
                       Package_Panel.Instance.itemDetailPanel.RightClick();
                }
            }
            
        }


        private void PutDownItem()
        {
            playerPackageSlot?.PutDownItem(currentUiPackageItem);
        }

        private void PickUpItem()
        {
            playerPackageSlot?.PickUpItem();
        }
    }
    
}
