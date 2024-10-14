using UnityEngine;

namespace GridSystem
{
    public class GridTesting : MonoBehaviour
    {

        public int width = 20;
        public int height = 10;
        public float cellSize = 10;
        private Grid<HeatMapObject> grid;
        void Start()
        {
            //3480 2160   
            int sceneWidth = 2560/40;
            int sceneHeight = 1440/40;
            grid = new Grid<HeatMapObject>(sceneWidth, sceneHeight,1,new Vector3(sceneWidth,sceneHeight)*-0.5f,(Grid<HeatMapObject> g,int x,int y) => new HeatMapObject(g,x,y));
            // HeatMapVisual heatMapVisual = new HeatMapVisual(grid, GetComponent<MeshFilter>());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && grid != null)
            {
                Vector3 mousePos = GetMousePos.GetMousePositionWithZ();
                var heatMapObject = grid.GetGridObject(mousePos);
                if(heatMapObject != null)
                    heatMapObject.AddValue(5);
                // grid.SetGridObject(mousePos, value + 5);
            }
        }

        
    }
    
}
