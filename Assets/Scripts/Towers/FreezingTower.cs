using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FreezingTower : Tower
{
    ManualDeltaTimer timer = new ManualDeltaTimer();

    public override void Start()
    {
        base.Start();
        timer.CallDown = SecondsOnAttack;
        timer.OnTick += OnTimerTick;
    }

    private void OnTimerTick()
    {
        var source = Instantiate(SourceOfDamagePrefab, transform);
        source.Init(Targets, Damage);
    }

    protected override void CheckTarget()
    {
        Targets = new List<Enemy>();
        var objects = Physics.OverlapBox(transform.position, new Vector3(Range / 2, Range / 2, Range / 2));
        foreach (var item in objects)
        {
            if (item.gameObject.GetComponent<Enemy>())
            {
                Targets.Add(item.gameObject.GetComponent<Enemy>());
            }
        }
    }

    protected override void Attack()
    {
        timer.Next(Time.deltaTime);
        if (Targets.Count != 0)
        {
            timer.TryToDoAction();
        }
    }
}