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
    

    public override void ApplyDamage(float damage)
    {
        Health -= damage;
        if (Health<=0)
        {
            Destroy(gameObject);
        }
        OnHealthChanged?.Invoke(Health);
    }

    private void OnDestroy()
    {
        ParentTile.CanBuild = true;
    }
}    
