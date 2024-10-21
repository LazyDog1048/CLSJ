using data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GridSystem
{
    public abstract class GridObjectSo : ScriptableObject
    {
        public string Name;
        public Sprite icon;
        [AssetsOnly]
        public GameObject prefab;
        // public GridGameObject prefab;
        public Shape_Data shapeData;
        public Vector2Int size=>shapeData.size;
    }
    
}
