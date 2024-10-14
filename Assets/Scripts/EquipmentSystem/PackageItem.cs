using data;
using game;
using GridSystem;
using UnityEngine;

namespace EquipmentSystem
{
    public class PackageItem : ClickableItem
    {
        [SerializeField]
        private data.PackageItemSoData equipmentSo;

        public data.PackageItemSoData EquipmentSo => equipmentSo;

        protected override void OnClick()
        {
            // Package_Panel.Instance.UpdatePreviewObject(this);
        }
    }
    
}
