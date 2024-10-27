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

        private void Awake()
        {
            enemyGeneratePoints = new List<EnemyGeneratePoint>();
            foreach (var enemyGeneratePoint in GetComponentsInChildren<EnemyGeneratePoint>())
            {
                enemyGeneratePoints.Add(enemyGeneratePoint);
            }
        }

        public void EnterRoom()
        {
            foreach (var enemyGeneratePoint in enemyGeneratePoints)
            {
                enemyGeneratePoint.EnterRoom();
            }
        }
        
        public void ExitRoom()
        {
            foreach (var enemyGeneratePoint in enemyGeneratePoints)
            {
                enemyGeneratePoint.ExitRoom();
            }
        }
    }
    
}
