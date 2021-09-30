using UnityEngine;

public abstract class Debuff : ScriptableObject
{
    public GameObject HandlerPrefab;
    public float Power;
    public float Duration = 0;
    public float TickCount;

    protected Enemy Target;
    protected GameObject handler;
    protected float TickDuration;

    private float timeLeft;
    private float timeLeftToTick;

    public bool IsOver => timeLeft < 0;

    public void Init(Enemy target)
    {
        Target = target;
        timeLeft = Duration;
        timeLeftToTick = TickDuration;
        TickDuration = Duration / TickCount;
        OnApply();
    }

    public virtual void OnApply()
    {
    }

    public abstract void OnDebuffTick();
    public abstract void OnDebuffEnd();

    public void Update()
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