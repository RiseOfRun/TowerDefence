using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DestrObjContainer : MonoBehaviour
{
    public DestroyableObject DO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (DO!=null)
        {
            Instantiate(DO, transform);
        }
    }
}
