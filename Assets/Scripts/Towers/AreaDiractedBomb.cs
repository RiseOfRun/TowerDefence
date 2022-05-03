using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AreaDiractedBomb : AOEBullet
{
    private Vector3 targetPosition;
    public void Init(List<Targetable> targets, Vector3 targetPosition, float damage, List<Debuff> debuffs = null)
    {
        base.Init(targets,damage,debuffs);
        this.targetPosition = targetPosition;
    }

    // public override void Init(List<Targetable> targets, float damage, List<Debuff> debuffs = null)
    // {
    //     base.Init(targets,damage,debuffs);
    //     if (targets.Count!=0)
    //     {
    //         targetPosition = targets[0].transform.position;
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    public override void Move()
    {
        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 
            Time.deltaTime * Speed);
        if (transform.position != targetPosition) return;
        DoAction();
    }
}
