using Sirenix.OdinInspector;
using so;
using UnityEngine;

namespace ui
{
    
    [CreateAssetMenu(fileName = "UiObjRefrenceSO", menuName = "Data/Refrence", order = 1)]
    public class UiObjRefrenceSO : SingletonSo<UiObjRefrenceSO>
    {
        [Header("CommonUi")]
        [AssetsOnly]
        public GameObject CommonUi_LayerPanelObj;
        [AssetsOnly]
        public GameObject PackagePanelObj;
        [AssetsOnly]
        public GameObject PlayerUiPanel;

    }

}
