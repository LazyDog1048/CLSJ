using game;
using UnityEngine;

namespace GridSystem
{
    public class GridGameObject : MonoPoolObj
    {
        public GridObjectSo gridObjectSo;
        
        public override string poolId => gridObjectSo.Name;
    }
    
}
