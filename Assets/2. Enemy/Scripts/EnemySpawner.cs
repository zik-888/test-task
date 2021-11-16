using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemySpawner : IInitializable
    {
        private readonly EnemyFactory _enemyFactory;
        private EnemySpawnMarker[] _enemyStartPoints;
        public List<EnemyInfo> EnemyInfos { private set; get; }

        public EnemySpawner(EnemyFactory enemyFactory, EnemySpawnMarker[] enemyStartPoints)
        {
            _enemyFactory = enemyFactory;
            _enemyStartPoints = enemyStartPoints;
        }
        
        public void Initialize()
        {
            EnemyInfos = new List<EnemyInfo>(_enemyStartPoints.Length);

            foreach (var point in _enemyStartPoints)
                EnemyInfos.Add(_enemyFactory.Create(point.Position));
        }
    }
}