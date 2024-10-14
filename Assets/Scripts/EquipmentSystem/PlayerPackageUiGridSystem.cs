using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class PlayerPackageUiGridSystem : PackageUiGridSystem
    {
        public override void PutDownItem(UiPackageItem item)
        {
            if(!item.CheckCanPut(Grid))
                return;
            item.PutOnGrid(this);
            item.SaveItemToPackage(this);
            PackageItemPreview.Instance.ClearItem();
        }
        
        public override void PickUpItem()
        {
            var mousePos = GetMousePos.GetMousePosition();
            UiGridObject uiGridObject = Grid.GetGridObject(mousePos);
            if (uiGridObject != null && uiGridObject.UiPackageItem != null)
            {
                var item = uiGridObject.UiPackageItem;
                item.PickOnGrid(Grid);
                item.RemoveItemToPackage(this);
                PackageItemPreview.Instance.SetPackageItem(item);
            }
        }
    }
    
}
