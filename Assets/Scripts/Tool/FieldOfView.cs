using System;
using System.Collections;
using System.Collections.Generic;
using game;
using Player;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private MeshCollider meshCollider;
    public float fov = 90;
    public int rayCount = 10;
    public float distance = 10;
    
    private float startAngle = 0;
    private Vector3 origin = Vector3.zero;
    private Mesh mesh;
    private MeshFilter meshFilter;
    private List<LightCollider> lightColliders;
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        lightColliders = new List<LightCollider>();
        mesh = new Mesh();
        meshCollider.sharedMesh = mesh;
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
        for(int i=0;i<rayCount;i++)
        {
            Vector3 vertex = origin +GetAngle.GetAngleFormVectorFloat(angle) * distance;
            var ray = Physics2D.Raycast(origin,GetAngle.GetAngleFormVectorFloat(angle), distance, layerMask);
            if (ray.collider == null)
            {
                vertex = origin +GetAngle.GetAngleFormVectorFloat(angle) * distance;
            }
            else
            {
                vertex = ray.point;
            }
            LightTouch(angle,ray.distance);
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

    public void LightTouch(float angle,float distance)
    {
        var hits = Physics2D.RaycastAll(origin, GetAngle.GetAngleFormVectorFloat(angle), distance, layerMask);

        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<LightCollider>() is LightCollider lightCollider)
            {
                
            }
            
        }
        
    }
    public void SetAimDirection(float angle)
    {
        startAngle = angle - fov / 2;
    }
    public void SetStartPos(Vector3 pos)
    {
        origin = pos;
    }
    
}
