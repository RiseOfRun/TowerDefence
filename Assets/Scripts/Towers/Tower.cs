using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Tower : MonoBehaviour
{
    public Transform SourcePosition;
    public List<Upgrade> Upgrades;
    public float Range;
    public float Damage=200;
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
        currentTime = 0;
        SecondsOnAttack = 1 / AttackPerSecond;
        GameEvents.TowerBuilt(this);
        timeToAttack = SecondsOnAttack;
        if (SourcePosition==null)
        {
            SourcePosition = transform;
        }
        // GameEvents.TowerBuilt(this);
        // GameEvents.OnEnemyKilled.Invoke(1);
        // Debug.Log($"towerDamage = {Damage}");
    }
    

    protected void CheckTarget()
    {
        Targets = new List<Enemy>();
        List<Enemy> selectedTargets = new List<Enemy>();
        if (CanForceTarget)
        {
            Targets.AddRange(TargetSystem.Instance.EnemyTargets.Where(
                x=>x!=null && Vector3.Distance(x.transform.position,transform.position)<=Range));
        }
        Collider[] objects = Physics.OverlapSphere(transform.position,
            Range,
            LayerMask.GetMask("Enemies"));
        foreach (Collider item in objects)
        {
            Enemy unit = item.GetComponent<Enemy>();
            if (Targets.Contains(unit)) continue;
            selectedTargets.Add(unit);
        }
        Targets.AddRange(selectedTargets.OrderByDescending(x=>x.PercentComplete));
    }

    public void Attack()
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
        Gizmos.DrawWireSphere(transform.position,Range);

    }
}
