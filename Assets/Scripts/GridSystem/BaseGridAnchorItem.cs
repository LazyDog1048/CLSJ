using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace item
{
    public class BaseGridAnchorItem<T> : MonoBehaviour where T: TransformGridObject<T>
    {
        
        public Vector2Int ItemSize => gridObjectSo.size;
        private T  anchorBuildingObject => gridObjectList[0];

        protected List<T> gridObjectList;
        protected GridObjectSo gridObjectSo;
        public GridObjectSo GridObjectSo => gridObjectSo;
        
        public Vector3 LeftUp => transform.position + new Vector3(-ItemSize.x / 2f, ItemSize.y / 2f);
        public Vector3 BottomCenter => transform.position + new Vector3(0, -ItemSize.y / 2f);
        // public Vector3 anchorPosition => anchorBuildingObject.GetWorldPosition();
        public Vector3 anchorPosition;
        public Vector3 centerPosition => anchorPosition - new Vector3(ItemSize.x / 2f, ItemSize.y / 2f);


        protected virtual void SetUp(Grid<T> grid,Vector3 choosePos,GridObjectSo so)
        {
            gridObjectSo = so;            
            Vector3 anchorPos = choosePos - new Vector3Int(so.size.x/2,so.size.y/2);
            gridObjectList = GetGridPositionList(grid,transform,anchorPos,so.size);

            foreach (var transformGridObject in gridObjectList)
            {
                transformGridObject.SetGridObjectSo(gridObjectSo);
            }
        }

        
        public void UpdateAnchorPosition()
        {
            transform.position = centerPosition;
        }
        
        public static List<T> GetGridPositionList(Grid<T> grid,Transform transform,Vector3 anchorPos,Vector2Int ItemSize) 
        {
            T anchorObject = grid.GetGridObject(anchorPos);
            List<T> gridList = new List<T>();
            T gridObject = null;
            for (int x = 0; x < ItemSize.x; x++) {
                for (int y = 0; y < ItemSize.y; y++) {
                    gridObject = grid.GetGridObject(anchorObject.X+ x, anchorObject.Y + y);
                    // Debug.Log($"{gridObject.X} {gridObject.Y} {gridObject.CanBuild()}");
                    gridObject.SetGridAnchorItem(transform);
                    gridList.Add(gridObject);
                }
            }
            return gridList;
        }
    }
    
}
