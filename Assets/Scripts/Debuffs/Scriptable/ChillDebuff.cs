using System;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = "Chilled debuff")]
[Serializable]
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
        if (similar != null)
        {
            if (similar.Power > Power)
            {
                Power = similar.Power;
            }

            Power = similar.Power;
            if (similar.Duration > Duration)
            {
                Duration = similar.Duration;
            }

            similar.OnDebuffEnd();
        }

        Target.Debuffs.Remove(similar);
        Target.Debuffs.Add(this);
        handler = Object.Instantiate(HandlerPrefab, Target.transform);
        Target.Speed /= Power;
    }
}