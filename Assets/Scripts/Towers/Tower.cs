using System;
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
    public Action OnShoot;
    private float SecondsOnAttack => 1 / Pattern.APS;
    private float timerToAttack;
    protected Collider[] TargetBuffer = new Collider[200];

    // Start is called before the first frame update
    public void Start()
    {
        GameEvents.TowerBuilt(this);
        timerToAttack = SecondsOnAttack;
        if (SourcePosition == null)
        {
            SourcePosition = transform;
        }

        // GameEvents.TowerBuilt(this);
        // GameEvents.OnEnemyKilled.Invoke(1);
        // Debug.Log($"towerDamage = {Damage}");
    }


    protected virtual void CheckTarget()
    {
        Targets.Clear();
        List<Enemy> selectedTargets = new List<Enemy>();
        if (CanForceTarget)
        {
            Targets.AddRange(TargetSystem.Instance.EnemyTargets.Where(
                x => x is Enemy && x != null &&
                     Vector3.Distance(x.transform.position, transform.position) <= Pattern.Range));
        }
        
        int count = Physics.OverlapSphereNonAlloc(transform.position,
            Pattern.Range, TargetBuffer,LayerMask.GetMask("Enemies"));
        for (int i = 0; i < count; i++)
        {
            Enemy unit = TargetBuffer[i].GetComponent<Enemy>();
            if (unit ==null || Targets.Contains(unit)) continue;
            selectedTargets.Add(unit);
        }

        Targets.AddRange(selectedTargets.Where(x => x.enabled)
            .OrderByDescending(x => x.PercentComplete));
        
        if (CanForceTarget)
        {
            Targets.AddRange(TargetSystem.Instance.EnemyTargets.Where(
                x => x is DestroyableObject && x != null
                && Vector3.Distance(x.transform.position, transform.position) <= Pattern.Range));
        }
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
        else if (timerToAttack < 0)
        {
            timerToAttack = 0;
        }
    }

    private void OnTimerTick()
    {
        SourceOfDamage source = Instantiate(Pattern.DamageSource, SourcePosition);
        source.Init(Targets, Pattern.Damage, Pattern.Debuffs);
        OnShoot?.Invoke();
    }

    public void Update()
    {
        CheckTarget();
        Attack();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Pattern.Range);
    }
}