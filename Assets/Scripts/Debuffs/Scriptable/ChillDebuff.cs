using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ChillDebuff : Debuff
{
    public ChillDebuff()
    {
    }

    public override void OnDebuffTick()
    {
    }
    
    

    public override void OnDebuffEnd()
    {
        Destroy(handler);
        Target.Speed *= Power;
        DurationTimer.Stop = true;
        Destroy(this);
    }

    public override void Tick()
    {
        DurationTimer.Next(Time.deltaTime);
        DurationTimer.TryToDoAction();
    }

    public override void OnApply()
    {
        Debuff similar = Target.Debuffs.Find(x => x.GetType() == GetType());
        if (similar!=null)
        {
            Power = similar.Power;
            if (similar.Duration>Duration)
            {
                Duration = similar.Duration;
            }
            similar.OnDebuffEnd();
        }
        Target.Debuffs.Add(this);
        handler = Instantiate(HandlerPrefub, Target.transform);
        Target.Speed /= Power;
    }
}
