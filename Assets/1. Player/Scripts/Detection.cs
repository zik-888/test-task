using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static System.String;

public class Detection : MonoBehaviour
{
    public LayerMask layerMaskDetectable;
    public LayerMask ignoreLayerMask;
    public event Action<int> Notify;
    
    private void FixedUpdate()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        bool isDetect = Physics.Raycast(ray, out var hit, 100f, layerMaskDetectable | ignoreLayerMask);
        
        if (!isDetect)
            NotDetectionHandler(ray);
        else
            DetectionHandler(hit, ray);
    }

    private void DetectionHandler(RaycastHit hit, Ray ray)
    {
        Notify?.Invoke(hit.transform.GetInstanceID());
        Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.blue);
    }

    private void NotDetectionHandler(Ray ray)
    {
        Notify?.Invoke(int.MaxValue);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.white);
    }

}
