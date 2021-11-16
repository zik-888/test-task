using Enemy;
using UnityEngine;
using Zenject;

public struct EnemyCounter
{
    public int Value;

    public EnemyCounter(int value) => Value = value;
}
public class EnemyFactory
{
    private GameObject _enemyPrefab;
    private DiContainer _container;
    private int _enemyCounter = 0;

    public EnemyFactory(DiContainer container)
    {
        _container = container;
    }

    public EnemyCounter GetEnemyCounter => new EnemyCounter(_enemyCounter);
    
    public void Load(GameObject enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;
    }

    public EnemyInfo Create(Vector3 enemyStartPoint)
    {
        var objectBindInfo = new GameObjectCreationParameters()
        {
            Name = $"Enemy-{_enemyCounter++}",
            Position = enemyStartPoint,
            Rotation = Quaternion.identity
        };
        
        var enemy = _container.InstantiatePrefab(_enemyPrefab, objectBindInfo);

        return new EnemyInfo() { Transform = enemy.transform };
    }
}