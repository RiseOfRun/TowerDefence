using System.Collections.Generic;
using UnityEngine;

public class CompoundTower : Tower
{
    private float timeToAttack;

    public override void Start()
    {
        base.Start();
        timeToAttack = SecondsOnAttack;
    }

    protected override void CheckTarget()
    {
        Targets = new List<Enemy>();
        Collider[] objects = Physics.OverlapBox(transform.position, 
            new Vector3(Range / 2, Range / 2, Range / 2),
            Quaternion.identity, 
            LayerMask.GetMask("Enemies"));
        foreach (Collider item in objects)
        {
            Targets.Add(item.GetComponent<Enemy>());
        }
    }

    protected override void Attack()
    {
        timeToAttack -= Time.deltaTime;
        if (Targets.Count != 0)
        {
            if (timeToAttack < 0)
            {
                OnTimerTick();
                timeToAttack += SecondsOnAttack;
            }
        }
        else
        {
            timeToAttack = 0;
        }
    }

    private void OnTimerTick()
    {
        var source = Instantiate(SourceOfDamagePrefab, transform);
        source.transform.position = SourcePosition.position;
        source.Init(Targets, Damage, DebuffsToApply);
    }
}