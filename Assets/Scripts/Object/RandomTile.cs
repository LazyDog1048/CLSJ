using UnityEngine;
using UnityEngine.Tilemaps;

namespace data
{
    public class RandomTile : Tile
    {
        [SerializeField]
        private int rate = 20;
        [SerializeField]
        public Sprite[] m_Sprites_1;
        
        [SerializeField]
        public Sprite[] m_Sprites_2;
        
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);
            
            if (Random.Range(0, 100) > rate)
            {
                if ((m_Sprites_1 != null) && (m_Sprites_1.Length > 0))
                {
                    int idx = Random.Range(0, m_Sprites_1.Length);
                    tileData.sprite = m_Sprites_1[idx];
                }
            }
            else
            {
                if ((m_Sprites_2 != null) && (m_Sprites_2.Length > 0))
                {
                    int idx = Random.Range(0, m_Sprites_2.Length);
                    tileData.sprite = m_Sprites_2[idx];
                }
            }
        }
    }
    
}
