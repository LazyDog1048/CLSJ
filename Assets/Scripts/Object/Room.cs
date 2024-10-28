using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace game
{
    public class Room : MonoBehaviour
    {
        [SerializeField]
        public string roomName;
        
        private List<EnemyGeneratePoint> enemyGeneratePoints;
        private List<BaseEnemy> _enemies;
        private void Awake()
        {
            enemyGeneratePoints = new List<EnemyGeneratePoint>();
            _enemies = new List<BaseEnemy>();
            foreach (var enemyGeneratePoint in GetComponentsInChildren<EnemyGeneratePoint>())
            {
                enemyGeneratePoints.Add(enemyGeneratePoint);
            }
        }

        public void EnterRoom()
        {
            foreach (var enemyGeneratePoint in enemyGeneratePoints)
            {
                enemyGeneratePoint.EnterRoom(this);
            }
            gameObject.SetActive(true);
        }
        
        public void ExitRoom()
        {
            foreach (var enemyGeneratePoint in enemyGeneratePoints)
            {
                enemyGeneratePoint.ExitRoom(this);
            }
            gameObject.SetActive(false);
        }
        
        
        public void AddEnemy(BaseEnemy enemy)
        {
            _enemies.Add(enemy);
        }
        
        public void RemoveEnemy(BaseEnemy enemy)
        {
            _enemies.Remove(enemy);    
        }
    }
    
}
