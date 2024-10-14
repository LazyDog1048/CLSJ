using System.Collections.Generic;
using data;
using EquipmentSystem;
using game;
using item;
using tool;
using ui;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GridSystem
{
    public class Package_Panel : SingletonPanel<Package_Panel>
    {
        public override LayerType TargetLayerType => LayerType.StaticUi;
        public static Vector2 CellSize = new Vector2(16, 16);
        private PackageUiGridSystem playerPackageUiGridSystem;
        private PackageUiGridSystem boxUiGridSystem;
        private PackageItemPreview _preview;


        private UiPackageItem uiItemOri;
        public Transform uiPackageItemPanel;
        
        public enum PackageState
        {
            none,
            select,
        }
        
        public static Package_Panel Load()
        {
            return Create(UiObjRefrenceSO.Instance.PackagePanelObj);
        }
        
        public Package_Panel(Transform trans) : base(trans)
        {
            uiPackageItemPanel = trans.Find("PackagePanel").Find("UiPackageItemPanel");
            uiItemOri = trans.Find("UiPackageItem").GetComponent<UiPackageItem>();
            playerPackageUiGridSystem = trans.Find("PackagePanel").GetComponentInChildren<PackageUiGridSystem>();
            boxUiGridSystem = trans.Find("BoxPanel").GetComponentInChildren<PackageUiGridSystem>();
            _preview = trans.GetComponentInChildren<PackageItemPreview>();
            var closeBtn = trans.Find("CloseBtn").GetComponent<Button>();
            
            playerPackageUiGridSystem.Init();
            boxUiGridSystem.Init();
            
            closeBtn.onClick.AddListener(Hide);
            GameManager.Instance.DelayExecute(0.1f,InitPackage);
        }

        public void InitPackage()
        {
            foreach (var item in LocalPackageThing.GetData().localPackageDataList)
            {
                UiPackageItem uiPackageItem = GameObject.Instantiate(uiItemOri);
                uiPackageItem.InitItem(playerPackageUiGridSystem,item);
            }
        }

        public void AddItemToBox(string name)
        {
            boxUiGridSystem.AddItem(name);
        }
 
        // private UiGridObject gridObject;
        public override void MouseMove(InputAction.CallbackContext context)
        {
            _preview.ItemUpdatePosition();
        }

        public override void Confirm(InputAction.CallbackContext context)
        {
            _preview.LeftClick(context);
        }

        public  void RightClick(InputAction.CallbackContext context)
        {
            _preview.RotateItem(context);
        }
    }
    
}
