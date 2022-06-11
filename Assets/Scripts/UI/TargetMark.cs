using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMark : MonoBehaviour
{
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        var anchor = transform.parent.GetComponent<Collider>();
        transform.position = anchor.bounds.center;
    }
    void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
    
    }
    
}
