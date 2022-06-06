using System.Collections.Generic;
using UnityEngine;

public class Bullet : SourceOfDamage
{
    public float Speed;
    public Targetable Target;
    // Start is called before the first frame update
    private Vector3 lastTargetPosition;
 
    public override void Init(List<Targetable> targets, float damage, List<Debuff> debuffs = null)
    {
        base.Init(targets, damage, debuffs);
        if (targets.Count!=0)
        {
            Target = targets[0];
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
