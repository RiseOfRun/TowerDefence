using System.Collections.Generic;
using UnityEngine;

public class Bullet : SourceOfDamage
{
    public float Speed;
    public Enemy Target;
    // Start is called before the first frame update
    private Vector3 lastTargetPosition;
    public void Init(int damage, Enemy target)
    {
        Damage = damage;
        Target = Targets[0];
        lastTargetPosition = target.transform.position;
    }
    public override void Init(List<Enemy> targets, float damage, List<Debuff> debuffs = null)
    {
        base.Init(targets, damage, debuffs);
        Target = targets[0];
    }
    public Bullet()
    {
        
    }
    void Move()
    {
        if (Target != null) 
        {
            lastTargetPosition = Target.transform.position;
            transform.LookAt(Target.transform.position);
        }
        transform.position = Vector3.MoveTowards(transform.position, lastTargetPosition, Time.deltaTime * Speed);
        if (transform.position != lastTargetPosition) return;
        if (Target!=null)
        {
            Target.ApplyDamage(Damage);
            ApplyDebuffs(Target);
        }
        Destroy(gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }
}
