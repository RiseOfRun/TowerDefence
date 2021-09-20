using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Bullet : MonoBehaviour, ISourceOfDamage
{
    public float Speed;
    public int Damage;
    public Enemy Target;
    // Start is called before the first frame update
    private Vector3 lastTargetPosition;
    public void Init(int damage, Enemy target)
    {
        Damage = damage;
        Target = target;
        lastTargetPosition = target.transform.position;
    }
    
    public Bullet()
    {
        
    }
    void Start()
    {
    }

    void Move()
    {
        if (Target != null) 
        {
            lastTargetPosition = Target.transform.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, lastTargetPosition, Time.deltaTime * Speed);
        if (transform.position != lastTargetPosition) return;
        if (Target!=null)
        {
            Target.ApplyDamage(Damage);
        }
        Destroy(gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
