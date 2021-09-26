using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Debuff : ScriptableObject
{
    public Enemy Target;
    public GameObject HandlerPrefub;
    public float Power;
    public float Duration = 0;
    public float TickCount;

    protected GameObject handler;
    protected float TickDuration;
    protected ManualDeltaTimer DebuffTickTimer = new ManualDeltaTimer();
    protected ManualDeltaTimer DurationTimer = new ManualDeltaTimer();

    public Debuff()
    {
    }

    public void Start()
    {
    }

    public virtual void Init(Enemy target, float power, GameObject debuffEffect, float duration = 0,
        float tickCount = 0)
    {
        HandlerPrefub = debuffEffect;
        Power = power;
        Target = target;
        Duration = duration;
        TickDuration = Duration / TickCount;

        DebuffTickTimer.CallDown = TickDuration;
        DebuffTickTimer.OnTick += OnDebuffTick;

        DurationTimer.CallDown = Duration;
        DurationTimer.LeastTime = Duration;
        DurationTimer.Capacity = 0;
        DurationTimer.OnTick += OnDebuffEnd;
        OnApply();
    }

    public virtual void OnApply()
    {
    }

    public abstract void OnDebuffTick();
    public abstract void OnDebuffEnd();

    public abstract void Tick();
}