using UnityEngine;

namespace GridSystem
{
    public class UiPackageManager : MonoBehaviour
    {
        [SerializeField]
        private PackageUiGridSystem packageUiGridSystem;
        [SerializeField]
        private PackageItemPreview preview;

        bool canBuild = false;
        Vector3 mousePos = Vector3.zero;

        // void Update()
        // {
        //     if(packageUiGridSystem.Grid == null)
        //         return;
        //     
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //
        //     }
        //
        //
        //     if (Input.GetMouseButton(0))
        //     {
        //         mousePos = GetMousePos.GetMousePositionWithZ();
        //         mousePos = packageUiGridSystem.Grid.WorldPositionToGridPosition(mousePos);
        //         mousePos.z = 0;
        //         // canBuild = UiGridObject.CheckCanBuild(uiGridSystem.Grid, testBuilding.size, mousePos, Lookdir.lookdir.down);
        //         UpdatePreviewBuilding(mousePos);
        //     }
        //     if (Input.GetMouseButtonUp(0))
        //     {
        //         if (canBuild)
        //         {
        //             // UiGridAnchorItem anchorItem = UiGridAnchorItem.Create(uiGridSystem.Grid,mousePos,Lookdir.lookdir.down,testBuilding);
        //         }
        //         HidePreviewBuilding();
        //     }
        // }
        //
        // private void SetPreviewBuilding(GridObjectSo so)
        // {
        //     previewObject.SetUp(so);
        // }
        //
        // private void UpdatePreviewBuilding(Vector3 mousePos)
        // {
        //     canBuild = previewObject.UpdatePosition(mousePos);
        // }
        //
        // private void HidePreviewBuilding()
        // {
        //     previewObject.Clear();
        // }
    }

}
