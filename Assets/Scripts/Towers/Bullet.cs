using System.Collections.Generic;
using UnityEngine;

public class Bullet : SourceOfDamage
{
    public float Speed;
    public Targetable Target;
    private Vector3 lastTargetPosition;
 
    public override void Init(List<Targetable> targets, float damage, List<Debuff> debuffs = null)
    {
        base.Init(targets, damage, debuffs);
        if (targets!=null)
        {
            Target = targets[0];
            transform.LookAt(Target.transform.position);
        }
    }
    
    public virtual void Move()
    {
        if (Target != null) 
        {
            lastTargetPosition = Target.transform.position;
            transform.LookAt(Target.transform.position);
        }
        transform.position = Vector3.MoveTowards(transform.position, lastTargetPosition, Time.deltaTime * Speed);
        if (transform.position != lastTargetPosition) return;
        DoAction();
        
    }

    public virtual void DoAction()
    {
        if (Target!=null)
        {
            Target.ApplyDamage(Damage);
            ApplyDebuffs(Target);
        }
        Destroy(gameObject);
    }
    
    public void Update()
    {
        Move();
    }
}
