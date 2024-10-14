using other;
using UnityEngine;

public class FieldOfView : Mono_Singleton<FieldOfView>
{
    [SerializeField]
    private LayerMask layerMask;
    public float fov = 90;
    public int rayCount = 10;
    public float distance = 10;
    
    private float startAngle = 0;
    private Vector3 origin = Vector3.zero;
    private Mesh mesh;
    private MeshFilter meshFilter;
    private float offset = 90;
    protected override void Awake()
    {
        base.Awake();
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
    }

    private void LateUpdate()
    {
        float angle = startAngle;
        float angleIncrease = fov / rayCount;
        meshFilter.mesh = mesh;
        
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];
        
        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i=0;i<=rayCount;i++)
        {
            Vector3 vertex = origin +GetAngle.GetAngleFormVectorFloat(angle) * distance;
            var ray = Physics2D.Raycast(origin,GetAngle.GetAngleFormVectorFloat(angle), distance, layerMask);
            if (ray.collider != null)
            {
                vertex = ray.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex -1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }
        
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void AimMode()
    {
        fov = 45;
        distance = 20;
        offset = 45;
    }
    public void NormalMode()
    {
        fov = 90;
        distance = 10;
        offset = 90;
    }
    public void SetAimDirection(float angle)
    {
        startAngle = angle - fov / 2 + offset;
    }
    public void SetStartPos(Vector3 pos)
    {
        origin = pos;
    }
    
}
