using item;
using UnityEngine;

namespace GridSystem
{
    public class GridBuildingSystem : MonoBehaviour
    {
        [SerializeField]
        private BuildingObjectSo testBuilding;
        [SerializeField]
        private Transform previewBuilding;
        
        public int gridWidth = 20;
        public int gridHeight = 10;
        public float cellSize = 10;
        private Grid<BuildingObject> grid;
        
        bool canBuild = false;
        Vector3 mousePos = Vector3.zero;
        
        private void Awake()
        {
            Vector3 offset = new Vector3(gridWidth, gridHeight) * -0.5f;
            grid = new Grid<BuildingObject>(gridWidth,gridHeight,cellSize,offset,NewBuildingObject);
            HeatMapVisual<BuildingObject> heatMapVisual = new HeatMapVisual<BuildingObject>(grid, GetComponent<MeshFilter>());
        }
        
        // Update is called once per frame
        void Update()
        {
            if(grid == null)
                return;
            
            if (Input.GetMouseButtonDown(0))
            {
                SetPreviewBuilding(testBuilding);
            }


            if (Input.GetMouseButton(0))
            {
                mousePos = GetMousePos.GetMousePositionWithZ();
                mousePos = grid.WorldPositionToGridPosition(mousePos);
                mousePos.z = 0;
                canBuild = BuildingObject.CheckCanBuild(grid, testBuilding.size, mousePos);
                UpdatePreviewBuilding(testBuilding,canBuild,mousePos);
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (canBuild)
                {
                    GridAnchorItem anchorItem = GridAnchorItem.Create(grid,mousePos,testBuilding);
                }
                HidePreviewBuilding();
                // var gridObj = grid.GetGridObject(mousePos);

                // if(gridObj != null)

                // grid.SetGridObject(mousePos, value + 5);
            }
        }
        
        private void SetPreviewBuilding(BuildingObjectSo so)
        {
            previewBuilding.gameObject.SetActive(true);
            previewBuilding.GetComponent<Animator>().runtimeAnimatorController =
                so.prefab.GetComponent<Animator>().runtimeAnimatorController;
        }
        
        private void UpdatePreviewBuilding(BuildingObjectSo so,bool canBuild,Vector3 mousePos)
        {
            Vector3 buildPos = mousePos - new Vector3Int(so.size.x/2,so.size.y/2);
            // previewBuilding.position = grid.GetGridObject(mousePos).GetWorldPosition();
            previewBuilding.position = buildPos;
            previewBuilding.Find("Body").GetComponent<SpriteRenderer>().color = canBuild ? Color.white : Color.red;
        }
        
        private void HidePreviewBuilding()
        {
            previewBuilding.gameObject.SetActive(false);
        }
        
        private BuildingObject NewBuildingObject(Grid<BuildingObject> grid,int x,int y)
        {
            return new BuildingObject(grid,x,y);
        }
    }
    
}
