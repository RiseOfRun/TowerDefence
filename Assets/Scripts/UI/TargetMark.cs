using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMark : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        var anchor = transform.parent.GetComponent<Collider>();
        transform.position = anchor.bounds.center;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    
    }
    
}
