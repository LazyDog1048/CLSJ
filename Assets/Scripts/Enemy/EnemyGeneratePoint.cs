using System;
using System.Collections.Generic;
using game;
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

        private void GenerateEnemy(Room room)
        {
            enemy = BaseEnemy.Load(enemySoData);
            enemy.transform.SetParent(this.transform);
            enemy.InitEnemy(PatrolPoints);
            room.AddEnemy(enemy);
        }
        
        public void EnterRoom(Room room)
        {
            if(enemy == null)
                GenerateEnemy(room);
            else
                enemy.SetActive(true);
        }
        
        public void ExitRoom(Room room)
        {
            if(enemy == null)
                return;
            enemy.SetActive(false);
        }
    }
    
}
