using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SourceOfDamage : MonoBehaviour
{
    [HideInInspector] public List<Targetable> Targets = new List<Targetable>();
    [HideInInspector] public float Damage = 0;
    public List<Debuff> DebuffsToApply;

    public virtual void Init(List<Targetable> targets, float damage, List<Debuff> debuffs = null)
    {
        DebuffsToApply = debuffs;
        Targets = targets.Where(x=> x!=null).ToList();
        Damage = damage;
    }

    public void ApplyDebuffs(Targetable target)
    {
        foreach (var debuff in DebuffsToApply)
        {
            var newDebuff = Instantiate(debuff);
            newDebuff.Init(target);
        }
    }
    
    
}