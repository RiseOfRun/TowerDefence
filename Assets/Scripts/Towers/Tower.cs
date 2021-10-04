using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tower : MonoBehaviour
{
    public Transform SourcePosition;
    public float Range;
    public int Damage=200;
    public int Cost;
    public bool CanForceTarget;
    public List<Debuff> DebuffsToApply = new List<Debuff>();
    [FormerlySerializedAs("targets")] public List<Enemy> Targets = new List<Enemy>();
    [FormerlySerializedAs("SourceOfDamage_Prefab")] public SourceOfDamage SourceOfDamagePrefab;
    public float AttackPerSecond = 1f;
    private float currentTime = 0;
    protected float SecondsOnAttack;
    private float timeToAttack;
    // Start is called before the first frame update
    public virtual void Start()
    {
        Debug.Log("Hello");
        currentTime = 0;
        SecondsOnAttack = 1 / AttackPerSecond;
        GameEvents.TowerBuilt(this);
        timeToAttack = SecondsOnAttack;
        // GameEvents.TowerBuilt(this);
        // GameEvents.OnEnemyKilled.Invoke(1);
        // Debug.Log($"towerDamage = {Damage}");
    }
    

    protected void CheckTarget()
    {
        Targets = new List<Enemy>();
        Collider[] objects = Physics.OverlapBox(transform.position, 
            new Vector3(Range / 2, Range / 2, Range / 2),
            Quaternion.identity, 
            LayerMask.GetMask("Enemies"));
        
        if (TargetSystem.Instance.EnemyTargets!=null)
        {
            Targets.Add(TargetSystem.Instance.EnemyTargets);
        }
        
        foreach (Collider item in objects)
        {
            Enemy unit = item.GetComponent<Enemy>();
            if (CanForceTarget && TargetSystem.Instance.EnemyTargets!=null)
            {
                if (unit==TargetSystem.Instance.EnemyTargets)
                {
                    continue;
                }
            }
            Targets.Add(unit);
        }
    }

    protected void Attack()
    {
        timeToAttack -= Time.deltaTime;
        if (Targets.Count != 0)
        {
            if (timeToAttack < 0)
            {
                OnTimerTick();
                timeToAttack += SecondsOnAttack;
            }
        }
        else
        {
            timeToAttack = 0;
        }
    }

    private void OnTimerTick()
    {
        var source = Instantiate(SourceOfDamagePrefab, transform);
        source.transform.position = SourcePosition.position;
        source.Init(Targets, Damage, DebuffsToApply);
    }
    IEnumerator DrawLaser()
    {
        while (Targets.Count != 0 && Targets[0] != null)
        {
            Debug.DrawLine(transform.position+new Vector3(0,1f,0) ,Targets[0].transform.position,Color.red);
            yield return null;
        }
    }

    public void Update()
    {
        CheckTarget();
        Attack();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(Range,1,Range));

    }
}
