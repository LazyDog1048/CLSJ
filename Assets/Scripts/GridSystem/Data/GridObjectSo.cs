using data;
using UnityEngine;

namespace GridSystem
{
    public abstract class GridObjectSo : ScriptableObject
    {
        public string Name;
        public Sprite icon;
        public GridGameObject prefab;
        public Shape_Data shapeData;
        public Vector2Int size=>shapeData.size;
    }
    
}
