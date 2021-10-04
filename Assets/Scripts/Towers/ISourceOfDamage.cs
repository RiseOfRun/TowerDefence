using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SourceOfDamage : MonoBehaviour
{
    public List<Enemy> Targets = new List<Enemy>();
    public float Damage = 0;
    public List<Debuff> DebuffsToApply;

    public virtual void Init()
    {
    }

    public virtual void Init(List<Enemy> targets, float damage, List<Debuff> debuffs = null)
    {
        DebuffsToApply = debuffs;
        Targets = targets.Where(x=> x!=null).ToList();
        Damage = damage;
    }

    public void ApplyDebuffs(Enemy target)
    {
        foreach (var debuff in DebuffsToApply)
        {
            var newDebuff = Instantiate(debuff);
            newDebuff.Init(target);
        }
    }
}