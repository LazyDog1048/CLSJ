using UnityEngine;

namespace GridSystem
{
    public class HeatMapVisual<T> where T : GridObject<T>
    {
        private Grid<T> grid;
        private Mesh mesh;

        public HeatMapVisual(Grid<T> grid, MeshFilter meshFilter)
        {
            this.grid = grid;
            mesh = new Mesh();
            meshFilter.mesh = mesh;
            UpdateHeatMap();
            grid.OnGridValueChanged += Grid_OnGridValueChanged;
        }

        private void Grid_OnGridValueChanged(object sender, Grid<T>.OnGridValueChangedEventArgs e)
        {
            UpdateHeatMap();
        }

        private void UpdateHeatMap()
        {
            MeshUtils.CreateEmptyMeshArrays(grid.Width * grid.Height, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles);

            for (int x = 0; x < grid.Width; x++) {
                for (int y = 0; y < grid.Height; y++) {
                    int index = x * grid.Height + y;
                    Vector3 baseSize = new Vector3(1, 1) * grid.CellSize;
                    var gridValue = grid.GetGridObject(x, y);
                    int maxGridValue = 100;
                    float gridValueNormalized = gridValue.GetNormalizedValue();
                    Vector2 gridCellUV = new Vector2(gridValueNormalized, 0f);
                    MeshUtils.AddToMeshArrays(vertices, uvs, triangles, index, grid.GetWorldPosition(x, y) + baseSize * .5f, 0f, baseSize, gridCellUV, gridCellUV);
                }
            }
            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles; 
        }
    }


    public class HeatMapObject : GridObject<HeatMapObject>
    {
        private const int MAX_VALUE = 100;
        private const int MIN_VALUE = 0;

        private int value;
        public int Value=>value;
        public HeatMapObject(Grid<HeatMapObject> grid,int x, int y) : base(grid,x,y)
        {
            value = 0;
        }

        public void AddValue(int addValue)
        {
            value = Mathf.Clamp(value + addValue, MIN_VALUE, MAX_VALUE);
            TriggerGridObjectChanged();
        }
        
        public float GetNormalizedValue()
        {
            return (float)value / MAX_VALUE;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
