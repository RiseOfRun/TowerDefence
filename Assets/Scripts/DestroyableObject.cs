using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DestroyableObject : Targetable
{
    // Start is called before the first frame update
    private Square ParentTile;
    void Start()
    {
        ParentTile = transform.parent.GetComponent<Square>();
        ParentTile.CanBuild = false;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        HandleDebuffs();
    }

    private void OnDestroy()
    {
        ParentTile.CanBuild = true;
    }
}    
