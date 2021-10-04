using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : Enemy
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
        
    }
    

    public void ApplyDamage(float count)
    {
        Health -= count;
        if (Health<=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        ParentTile.CanBuild = true;
    }
}    
