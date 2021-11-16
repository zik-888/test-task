using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FinishLevelMarker : MonoBehaviour
{
    private Collider _heroCollider;
    private ILevelEventHandler _levelEventHandler;
    
    [Inject]
    private void Construct(Detection heroDetection, ILevelEventHandler levelEventHandler)
    {
        _heroCollider = heroDetection.gameObject.GetComponent<Collider>();
        _levelEventHandler = levelEventHandler;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other == _heroCollider)
        {
            print("Hero - OnTriggerEnter");
            _levelEventHandler.MissionEventInvoke(MissionPart.FinishMarkerAchieved, gameObject);
        }
    }

}
