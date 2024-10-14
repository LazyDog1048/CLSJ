using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class GridGameObject : MonoBehaviour
    {
        [SerializeField]
        public GridObjectSo gridObjectSo;

        public Vector3 offset => new Vector3(-gridObjectSo.size.x / 2f, gridObjectSo.size.y / 2f);

        public Vector3 LeftUpPos => transform.position + offset;
    }
    
}
