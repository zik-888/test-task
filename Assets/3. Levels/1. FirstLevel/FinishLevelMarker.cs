using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FinishLevelMarker : MonoBehaviour
{
    private Collider _heroCollider;
    private ILevelController _levelController;
    
    [Inject]
    private void Construct(Detection heroDetection, ILevelController levelController)
    {
        _heroCollider = heroDetection.gameObject.GetComponent<Collider>();
        _levelController = levelController;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other == _heroCollider)
        {
            print("Hero - OnTriggerEnter");
            _levelController.MissionEventInvoke(MissionPart.FinishMarkerAchieved, gameObject);
        }
    }

}
