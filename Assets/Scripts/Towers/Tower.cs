using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Tower : MonoBehaviour
{
    public Transform SourcePosition;
  
    public bool CanForceTarget;
    [FormerlySerializedAs("targets")] public List<Targetable> Targets = new List<Targetable>();
    public TowerPattern Pattern;
    private float SecondsOnAttack => 1 / Pattern.APS;
    private float timerToAttack;
    // Start is called before the first frame update
    public virtual void Start()
    {
        GameEvents.TowerBuilt(this);
        timerToAttack = SecondsOnAttack;
        if (SourcePosition==null)
        {
            SourcePosition = transform;
        }
        // GameEvents.TowerBuilt(this);
        // GameEvents.OnEnemyKilled.Invoke(1);
        // Debug.Log($"towerDamage = {Damage}");
    }


    private void CheckTarget()
    {
        Targets = new List<Targetable>();
        List<Enemy> selectedTargets = new List<Enemy>();
        if (CanForceTarget)
        {
            Targets.AddRange(TargetSystem.Instance.EnemyTargets.Where(
                x=>x!=null && Vector3.Distance(x.transform.position,transform.position)<=Pattern.Range));
        }
        Collider[] objects = Physics.OverlapSphere(transform.position,
            Pattern.Range,
            LayerMask.GetMask("Enemies"));
        foreach (Collider item in objects)
        {
            Enemy unit = item.GetComponent<Enemy>();
            if (Targets.Contains(unit)) continue;
            selectedTargets.Add(unit);
        }
        Targets.AddRange(selectedTargets.Where(x => x!=null && x.enabled).
            OrderByDescending(x => x.PercentComplete));
    }

    public void Attack()
    {
        timerToAttack -= Time.deltaTime;
        if (Targets.Count != 0)
        {
            if (timerToAttack < 0)
            {
                OnTimerTick();
                timerToAttack += SecondsOnAttack;
            }
        }
        else
        {
            timerToAttack = 0;
        }
    }

    private void OnTimerTick()
    {
        SourceOfDamage source = Instantiate(Pattern.DamageSource, transform);
        source.transform.position = SourcePosition.position;
        source.Init(Targets, Pattern.Damage, Pattern.Debuffs);
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
        Gizmos.DrawWireSphere(transform.position,Pattern.Range);

    }
}
