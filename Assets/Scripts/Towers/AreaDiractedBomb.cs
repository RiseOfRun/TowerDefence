using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class AreaDiractedBomb : AOEBullet
{
    private Vector3 targetPosition;
    public void Init(List<Targetable> targets, Vector3 targetPosition, float damage, List<Debuff> debuffs = null)
    {
        DebuffsToApply = debuffs;
        this.targetPosition = targetPosition;
        Damage = damage;
    }

    
    public override void Move()
    {
        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 
            Time.deltaTime * Speed);
        if (transform.position != targetPosition) return;
        DoAction();
    }
}
