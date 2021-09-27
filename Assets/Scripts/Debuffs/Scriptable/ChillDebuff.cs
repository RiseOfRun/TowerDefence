using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ChillDebuff : Debuff
{
    public override void OnDebuffTick()
    {
    }
    
    public override void OnDebuffEnd()
    {
        Object.Destroy(handler);
        Target.Speed *= Power;
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
        handler = Object.Instantiate(HandlerPrefab, Target.transform);
        Target.Speed /= Power;
    }
}
