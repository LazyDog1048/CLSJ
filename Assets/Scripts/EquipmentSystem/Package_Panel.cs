using System.Collections.Generic;
using System.Globalization;
using EquipmentSystem;
using game;
using Player;
using TMPro;
using ui;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GridSystem
{
    public class Package_Panel : SingletonPanel<Package_Panel>
    {
        // public override LayerType TargetLayerType => LayerType.StaticUi;
        // public static Vector2 CellSize = new Vector2(16, 16);
        public PlayerPackageUiGridSystem playerPackageUiGridSystem;
        public PackageUiGridSystem boxUiGridSystem;
        private PackageItemPreview _preview;

        // private PlayerEquipmentSlot[] playerEquipmentSlots;
        public UiPackageItem uiItemOri;

        public WeaponDetailPanel weaponDetailPanel;
        public PackageItemDetailPanel packageItemDetailPanel;
        public ItemFunctionPanel itemFunctionPanel;
        public static Package_Panel Load()
        {
            return Create(UiObjRefrenceSO.Instance.PackagePanelObj);
        }
        
        public Package_Panel(Transform trans) : base(trans)
        {
            // playerEquipmentSlots = trans.Find("EquipmentPanel").GetComponentsInChildren<PlayerEquipmentSlot>();
            uiItemOri = trans.Find("UiPackageItem").GetComponent<UiPackageItem>();
            playerPackageUiGridSystem = trans.Find("PackagePanel").GetComponentInChildren<PlayerPackageUiGridSystem>();
            boxUiGridSystem = trans.Find("BoxPanel").GetComponentInChildren<PackageUiGridSystem>();
            
            weaponDetailPanel = new WeaponDetailPanel(trans.Find("WeaponDetailPanel"));
            packageItemDetailPanel = new PackageItemDetailPanel(trans.Find("PackageItemDetailPanel"));
            itemFunctionPanel = new ItemFunctionPanel(trans.Find("ItemFunctionPanel"));
            _preview = trans.GetComponentInChildren<PackageItemPreview>();
            var closeBtn = trans.Find("CloseBtn").GetComponent<Button>();
            playerPackageUiGridSystem.Init();
            boxUiGridSystem.Init();
            
            closeBtn.onClick.AddListener(() =>
            {
                Debug.Log("close");
                Hide();
            });
        }

        private void LoadPlayerPackage()
        {
            if(playerPackageUiGridSystem == null)
                return;
            LocalPackageThing.GetData().LoadPlayerPackage(uiItemOri,playerPackageUiGridSystem);
        }

        public void AddPreviewItem(PackageItemData packageItemData)
        {
            UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
            uiPackageItem.InitItem(packageItemData);
            PackageItemPreview.Instance.SetPackageItem(uiPackageItem);
        }
        
        protected override void OnShowAction()
        {
            base.OnShowAction();
            boxUiGridSystem.ClearItem();
            LoadPlayerPackage();
        }

        protected override void OnHideAction()
        {
            Debug.Log("OnHideAction");
            base.OnHideAction();
            if(playerPackageUiGridSystem == null)
                return;
            playerPackageUiGridSystem.ClosePanelSaveData();
            // foreach (var playerEquipmentSlot in playerEquipmentSlots)
            // {
            //     playerEquipmentSlot.SavePlayerEquipmentSlotData();
            // }
            PlayerController.Instance.playerEquipment.UpdateEquipment();
            boxUiGridSystem.ClearItem();
            playerPackageUiGridSystem.ClearItem();
            HidePreview();
        }

 
        // private UiGridObject gridObject;
        public override void MouseMove(InputAction.CallbackContext context)
        {
            // var mousePos = GetMousePos.GetMousePosition();
            // boxUiGridSystem.Grid.GetXY(mousePos, out int x, out int y);
            // Debug.Log($"{x}  {y}");
            _preview.ItemUpdatePosition();
        }

        public override void Confirm(InputAction.CallbackContext context)
        {
            _preview.LeftClick(context);
        }

        public  void RightClick(InputAction.CallbackContext context)
        {
            _preview.RightClick(context);

        }
        
        public void HidePreview()
        {
            itemFunctionPanel.ExitItemPanel();
            packageItemDetailPanel.ExitItemPanel();
            weaponDetailPanel.ExitItemPanel();
        }
    }
    
    public class BaseItemPanel:BasePanel
    {
        protected TextMeshProUGUI nameText;
        protected TextMeshProUGUI typeText;
        protected TextMeshProUGUI rareText;
        
        protected TextMeshProUGUI priceText;
        protected TextMeshProUGUI weightText;
        protected Vector3 offset = new Vector3(109,0,0);
        
        protected BaseItemPanel(Transform trans) : base(trans)
        {
            nameText = trans.Find("NameText").GetComponent<TextMeshProUGUI>();
            typeText = trans.Find("TypeText").GetComponent<TextMeshProUGUI>();
            rareText = trans.Find("RareText").GetComponent<TextMeshProUGUI>();
            
            priceText = trans.Find("PriceText").GetComponent<TextMeshProUGUI>();
            weightText = trans.Find("WeightText").GetComponent<TextMeshProUGUI>();
        }
        
        public virtual void EnterItemPanel(UiPackageItem item)
        {
            Vector3 itemSize = item.rectTransform.sizeDelta;
            transform.position = item.transform.position + new Vector3((itemSize.x+offset.x)/2/13.33f,0,0);
            var data = item.packageItemSoData;
            nameText.text = data.Name;
            typeText.text = data.ItemType.ToString();
            rareText.text = data.quality.ToString();
            priceText.text = data.price.ToString();
            weightText.text = data.weight.ToString(CultureInfo.InvariantCulture);
        }
        
        public void ExitItemPanel()
        {
            transform.position = new Vector3(-1000,-1000,0);
        }
    }
    public class WeaponDetailPanel:BaseItemPanel
    {
        TextMeshProUGUI damageText;
        TextMeshProUGUI shotDelayText;
        TextMeshProUGUI shotStabilityText;
        TextMeshProUGUI shotCalibrationText;
        TextMeshProUGUI bulletSpeedText;
        TextMeshProUGUI reloadTimeText;
        TextMeshProUGUI maxAmmoText;

        public WeaponDetailPanel(Transform trans) : base(trans)
        {
            damageText = trans.Find("DamagePanel").Find("ValueText").GetComponent<TextMeshProUGUI>();
            shotDelayText = trans.Find("ShotDelayPanel").Find("ValueText").GetComponent<TextMeshProUGUI>();
            shotStabilityText = trans.Find("ShotStabilityPanel").Find("ValueText").GetComponent<TextMeshProUGUI>();
            shotCalibrationText = trans.Find("ShotCalibrationPanel").Find("ValueText").GetComponent<TextMeshProUGUI>();
            bulletSpeedText = trans.Find("BulletSpeedPanel").Find("ValueText").GetComponent<TextMeshProUGUI>();
            reloadTimeText = trans.Find("ReloadTimePanel").Find("ValueText").GetComponent<TextMeshProUGUI>();
            maxAmmoText = trans.Find("MaxAmmoPanel").Find("ValueText").GetComponent<TextMeshProUGUI>();
        }
        
        public override void EnterItemPanel(UiPackageItem item)
        {
            base.EnterItemPanel(item);
            GunData gunData = item.packageItemSoData as GunData;
            typeText.text = gunData.shotType.ToString();
            
            damageText.text = gunData.Damage.ToString();
            shotDelayText.text = gunData.shotDelay.ToString(CultureInfo.InvariantCulture);
            shotStabilityText.text = gunData.shotStability.ToString(CultureInfo.InvariantCulture);
            shotCalibrationText.text = gunData.shotCalibration.ToString(CultureInfo.InvariantCulture);
            bulletSpeedText.text = gunData.bulletSpeed.ToString(CultureInfo.InvariantCulture);
            reloadTimeText.text = gunData.reloadTime.ToString(CultureInfo.InvariantCulture);
            maxAmmoText.text = gunData.maxAmmo.ToString();
        }
        
    }

    public class PackageItemDetailPanel : BaseItemPanel
    {
        private TextMeshProUGUI itemDescription_Text;
        Vector3 offset = new Vector3(90,0,0);
        public PackageItemDetailPanel(Transform trans) : base(trans)
        {
            itemDescription_Text = trans.Find("ItemDescription_Panel").Find("NameText").GetComponent<TextMeshProUGUI>();
        }
        
        public override void EnterItemPanel(UiPackageItem item)
        {
            
            base.EnterItemPanel(item);
            var data = item.packageItemSoData;
            itemDescription_Text.text = data.description;
        }
    }
    
    public class ItemFunctionPanel:BasePanel
    {
        public UiPackageItem item;
        protected Button unpackBtn;
        
        protected Vector3 offset = new Vector3(109,0,0);
        protected bool isShow;
        public ItemFunctionPanel(Transform trans) : base(trans)
        {
            isShow = false;
            unpackBtn = trans.Find("UnpackBtn").GetComponent<Button>();
            unpackBtn.onClick.AddListener(UnpackItem);
        }
        
        public void CheckItemFunctionPanel()
        {
            if(isShow)
            {
                ExitItemPanel();
            }
            else
            {
                EnterItemPanel();
            }
        }
        public void EnterItemPanel()
        {
            isShow = true;
            item = UiPackageItem.cursorUiPackageItem;
            Vector3 itemSize = item.rectTransform.sizeDelta;
            transform.position = item.transform.position + new Vector3((itemSize.x+offset.x)/2/13.33f,0,0);
            Package_Panel.Instance.weaponDetailPanel.ExitItemPanel();
            Package_Panel.Instance.packageItemDetailPanel.ExitItemPanel();
        }
        
        public void ExitItemPanel()
        {
            if (UiPackageItem.cursorUiPackageItem != null)
            {
                switch (UiPackageItem.cursorUiPackageItem.packageItemSoData)
                {
                    case GunData gunData:
                        Package_Panel.Instance.weaponDetailPanel.EnterItemPanel(UiPackageItem.cursorUiPackageItem);
                        break;
                    default:
                        Package_Panel.Instance.packageItemDetailPanel.EnterItemPanel(UiPackageItem.cursorUiPackageItem);
                        break;
                }
            }
            transform.position = new Vector3(-1000,-1000,0);
            isShow = false;
        }
        
        public void UnpackItem()
        {
            if(item.Count <= 1)
                return;
            int halfCount = item.Count / 2;
            item.Count -= halfCount;
            SceneBox.Instance.AddPreview(item.packageItemSoData.Name,halfCount);
        }
    }
}
