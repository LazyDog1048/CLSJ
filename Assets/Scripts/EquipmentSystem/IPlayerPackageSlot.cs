using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public interface IPlayerPackageSlot
    {
        void PutDownItem(UiPackageItem item);
        void PickUpItem();
    }
    
}
