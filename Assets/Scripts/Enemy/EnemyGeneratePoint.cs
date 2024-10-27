using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyGeneratePoint : MonoBehaviour
    {
        protected List<Vector3> PatrolPoints;
        
        [SerializeField]
        private EnemySoData enemySoData;
        private BaseEnemy enemy;

        private void Awake()
        {
            PatrolPoints = new List<Vector3>();
            var patrol = transform.Find("Patrol");
            for (int i = 0; i < patrol.childCount; i++)
            {
                PatrolPoints.Add(patrol.GetChild(i).position);
            }
        }

        private void GenerateEnemy()
        {
            enemy = BaseEnemy.Load(enemySoData);
            enemy.InitEnemy(PatrolPoints);
        }
        
        public void EnterRoom()
        {
            GenerateEnemy();
        }
        
        public void ExitRoom()
        {
            enemy.ReleaseObj();
        }
    }
    
}
