using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;
using Zenject;

public class FirstLevelInstaller : MonoInstaller
{
    public Transform heroStartPoint;
    public GameObject heroPrefab;

    public Transform[] enemyStartPoints;
    public GameObject enemyPrefab;

    public Transform[] leaveMarkers;

    public FirstLevelController missionController;

    public override void InstallBindings()
    {
        BindEnemy();
        BindHero();
        BindMissionController();
        BindLeaveMarkers();
    }

    private void BindMissionController()
    {
        Container
            .Bind<ILevelController>()
            .FromInstance(missionController)
            .AsSingle().NonLazy();
    }

    private void BindEnemy()
    {
        Container
            .BindInterfacesAndSelfTo<EnemySpawner>()
            .AsSingle().NonLazy();

        var enemyFactory = new EnemyFactory(Container);
        enemyFactory.Load(enemyPrefab);
        
        Container
            .Bind<EnemyFactory>()
            .FromInstance(enemyFactory)
            .AsSingle().NonLazy();

        Container
            .Bind<EnemySpawnMarker[]>()
            .FromInstance(enemyStartPoints
                .Select(p => new EnemySpawnMarker(p.position))
                .ToArray())
            .AsSingle().NonLazy();
    }

    private void BindLeaveMarkers()
    {
        Container
            .Bind<EnemyLeaveMarker[]>()
            .FromInstance(leaveMarkers
                .Select(l => new EnemyLeaveMarker() 
                    { LeavePosition = l.position } )
                .ToArray())
            .AsSingle().NonLazy();
    }
    private void BindHero()
    {
        var heroDetection = Container
            .InstantiatePrefabForComponent<Detection>(
                heroPrefab,
                heroStartPoint.position,
                Quaternion.identity,
                null);

        Container
            .Bind<Detection>()
            .FromInstance(heroDetection)
            .AsSingle().NonLazy();
    }
}