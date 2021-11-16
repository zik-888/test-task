using System;
using System.Collections;
using System.Collections.Generic;
using _3._Levels;
using Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using static System.String;

public enum MissionPart
{
    OneEnemyDestroy,
    FinishMarkerAchieved,
    EnemyReachedHero
}

public class FirstLevelEventHandler : MonoBehaviour, ILevelEventHandler
{
    private event Action<MissionPart, GameObject> MissionEvent;
    
    public FinishLevelMarker finishLevelMarker;
    public Text enemyTextCounter;

    private int _enemyCounter;
    private EnemySpawner _enemySpawner;


    [Inject]
    private void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    private void Start()
    {
        _enemyCounter = _enemySpawner.EnemyInfos.Count;
        Cursor.lockState = CursorLockMode.Locked;
        MissionEvent += MissionEventHandler;
        enemyTextCounter.text = $"Врагов осталось: {_enemyCounter}";
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void MissionEventHandler(MissionPart missionPart, GameObject obj)
    {
        switch (missionPart)
        {
            case MissionPart.FinishMarkerAchieved:
                FinishMarkerAchieved();
                break;
            case MissionPart.OneEnemyDestroy:
                OneEnemyDestroy(obj);
                break;
            case MissionPart.EnemyReachedHero:
                EnemyReachedHero();
                break;
        }
    }

    private void FinishMarkerAchieved()
    {
        if (_enemyCounter == 0)
        {
            print("MissionComplete");
            LevelScore.Status = $"Mission complete!";
        }
        else
        {
            print("MissionFailed");
            LevelScore.Status = "Game over!\n" +
                                "Не все враги обнаружены";
        }
        
        LevelScore.Score = Empty;
        
        foreach (var enemy in _enemySpawner.EnemyInfos)
            LevelScore.Score += $"{enemy.Transform.name} - {enemy.GetDetectedStatus()} \n";

        SceneManager.LoadScene(2);
    }

    private void EnemyReachedHero()
    {
        print("GameOver");
        
        LevelScore.Status = "Game over!\n" +
                            "К вам приблизился враг";
        
        LevelScore.Score = Empty;
        
        foreach (var enemy in _enemySpawner.EnemyInfos)
            LevelScore.Score += $"{enemy.Transform.name} - {enemy.GetDetectedStatus()} \n";

        SceneManager.LoadScene(2);
    }

    private void OneEnemyDestroy(GameObject o)
    {
        foreach (var enemy in _enemySpawner.EnemyInfos)
            if (enemy.Transform.name == o.name)
                enemy.IsDetected = true;

        _enemyCounter--;
        print($"OneEnemyDestroy: {_enemyCounter}");
        enemyTextCounter.text = $"Врагов осталось: {_enemyCounter}";
    }

    public void MissionEventInvoke(MissionPart missionPart, GameObject obj)
    {
        MissionEvent?.Invoke(missionPart, obj);
    }
}