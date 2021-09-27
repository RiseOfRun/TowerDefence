using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;

public abstract class Debuff
{
    public Enemy Target;
    public GameObject HandlerPrefab;
    public float Power;
    public float Duration = 0;
    public float TickCount;

    protected GameObject handler;
    protected float TickDuration;

    private float timeLeft;
    private float timeLeftToTick;

    public bool IsOver => timeLeft < 0;

    public void Init(Enemy target, float power, GameObject debuffEffect, float duration = 0, float tickCount = 0)
    {
        HandlerPrefab = debuffEffect;
        Power = power;
        Target = target;
        Duration = duration;
        timeLeft = duration;
        timeLeftToTick = TickDuration;
        TickDuration = Duration / TickCount;
        OnApply();
    }

    public virtual void OnApply()
    {
    }

    public abstract void OnDebuffTick();
    public abstract void OnDebuffEnd();

    public virtual void Update()
    {
        timeLeft -= Time.deltaTime;
        timeLeftToTick -= Time.deltaTime;

        if (timeLeftToTick < 0)
        {
            OnDebuffTick();
            timeLeftToTick += TickDuration;
        }

        if (timeLeft < 0)
        {
            OnDebuffEnd();
        }
    }
}