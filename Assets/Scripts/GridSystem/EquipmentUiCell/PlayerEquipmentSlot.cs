using System;
using data;
using EquipmentSystem;
using game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GridSystem
{
    public enum SlotState
    {
        Empty,
        Occupied
    }
    public class PlayerEquipmentSlot : MonoBehaviour,IPlayerPackageSlot,IPointerEnterHandler,IPointerExitHandler
    {
        private Image icon;
        private Image bg;
        public PackageItemData packageItemData { get;protected set; }
        public RectTransform rectTransform { get;private set; }
        public PackageItemSoData SoData => packageItem == null? null : packageItem.packageItemSoData;
        public string itemName => SoData == null ? "" : SoData.Name;
            
        public virtual PackageItemType ItemType => PackageItemType.Other;
        protected UiPackageItem packageItem;
        public SlotState State { get; private set; }

        private void Awake()
        {
            icon = transform.Find("Icon").GetComponent<Image>();
            bg = transform.Find("Bg").GetComponent<Image>();
            rectTransform = transform as RectTransform;
            State = SlotState.Empty;
        }

        public void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            if (packageItemData != null && !packageItemData.Name.Equals(""))
            {
                var packageItemSoData = ResourcesDataManager.GetPackageItemSoData(packageItemData.Name);
                UiPackageItem uiPackageItem = Instantiate(Package_Panel.Instance.uiItemOri);
                uiPackageItem.InitItem(this, packageItemSoData);
                PutDownItem(uiPackageItem);
            }
        
        }

        public virtual void PutDownItem(UiPackageItem item)
        {
            if(ItemType != item.packageItemSoData.ItemType)
                return;
            if (State == SlotState.Empty)
            {
                PutDownOnSlot(item);
            }
            else if (State == SlotState.Occupied)
            {
                var oldItem = packageItem;
                PickUpItem();
                PutDownOnSlot(item);
                PackageItemPreview.Instance.SetPackageItem(oldItem);
            }
        }
        
        public void PutDownOnSlot(UiPackageItem item)
        {
            icon.gameObject.SetActive(true);
            bg.gameObject.SetActive(true);
            
            State = SlotState.Occupied;
            icon.sprite = item.packageItemSoData.icon;
            // Vector2 sizeDelta =  new Vector2(item.packageItemSoData.size.x,item.packageItemSoData.size.y) * StaticValue.pixelsPerUnit;
            // icon.rectTransform.sizeDelta = sizeDelta;
            
            packageItem = item;
            packageItemData = item.packageItemData;
            item.PutOnSlot(this);
            packageItem.gameObject.SetActive(false);
            PackageItemPreview.Instance.ClearItem();
        }
        
        public virtual void PickUpItem()
        {
            if(State == SlotState.Empty)
                return;
            State = SlotState.Empty;

            packageItem.PickOnSlot(this);
            packageItem.gameObject.SetActive(true);
            PackageItemPreview.Instance.SetPackageItem(packageItem);
            packageItem = null;
            icon.gameObject.SetActive(false);
            bg.gameObject.SetActive(false);
            
        }
        
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            PackageItemPreview.Instance.SwitchSlot(this);
            CheckOnPointerEnter();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if(!UiPackageItem.InDetailPanel)
                PackageItemPreview.Instance.RemoveSlot();
            CheckPointerExit();
        }

        private void CheckOnPointerEnter()
        {
            if(packageItem == null || PackageItemPreview.Instance.currentUiPackageItem != null)
                return;
            Package_Panel.Instance.itemDetailPanel.EnterItemPanel(packageItem);
        }

        public void CheckPointerExit()
        {
            Package_Panel.Instance.itemDetailPanel.ExitItemPanel();
        }
        public virtual void SavePlayerEquipmentSlotData()
        {
            
        }
    }
    
}
