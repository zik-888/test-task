using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static System.String;

[RequireComponent(typeof(EnemyMovement))]
public class Detectable : MonoBehaviour
{
    public float detectionTimeInSeconds = 3f;

    [SerializeField]
    private Image image;
    
    private float _timeLeft = 0;
    private DetectCoroutineState _coroutineState = DetectCoroutineState.NotWork;
    public DetectableState State;
    private float NormalizeRemainder => _timeLeft / detectionTimeInSeconds;


    [Inject]
    private void Construct(Detection heroDetection, ILevelController levelController)
    {
        heroDetection.Notify += DetectableHandler;
        var enemyMovement = GetComponent<EnemyMovement>();
        State = new NotDetectedState(image, levelController, enemyMovement);
    }

    private void DetectableHandler(int enemyId)
    {
        if (enemyId == transform.GetInstanceID())
        {
            if (_coroutineState == DetectCoroutineState.StartDetectIsWork) return;
            _coroutineState = DetectCoroutineState.StartDetectIsWork;
            StopAllCoroutines();
            StartCoroutine(StartDetect());
        }
        else
        {
            if (_coroutineState == DetectCoroutineState.CountdownIsWork || !(NormalizeRemainder > 0)) return;
            _coroutineState = DetectCoroutineState.CountdownIsWork;
            StopAllCoroutines();
            StartCoroutine(Countdown());
        }
    }
    private IEnumerator StartDetect()
    {
        while (NormalizeRemainder < 1)
        {
            _timeLeft += Time.deltaTime;
            State.ChangeState(this, NormalizeRemainder);
            yield return new WaitForEndOfFrame();
        }
        
        _coroutineState = DetectCoroutineState.NotWork;
    }

    public IEnumerator Countdown()
    {
        while (NormalizeRemainder > 0)
        {
            _timeLeft -= Time.deltaTime;
            State.ChangeState(this, NormalizeRemainder);
            yield return new WaitForEndOfFrame();
        }
        
        _coroutineState = DetectCoroutineState.NotWork;
    }

    private enum DetectCoroutineState
    {
        NotWork,
        StartDetectIsWork,
        CountdownIsWork
    }
}