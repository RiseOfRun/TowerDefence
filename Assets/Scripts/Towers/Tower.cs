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
    public List<Debuff> DebuffsToApply = new List<Debuff>();
    [FormerlySerializedAs("targets")] public List<Enemy> Targets = new List<Enemy>();
    [FormerlySerializedAs("SourceOfDamage_Prefab")] public SourceOfDamage SourceOfDamagePrefab;
    public float AttackPerSecond = 1f;
    private float currentTime = 0;
    protected float SecondsOnAttack;
    // Start is called before the first frame update
    public virtual void Start()
    {
        Debug.Log("Hello");
        currentTime = 0;
        SecondsOnAttack = 1 / AttackPerSecond;
        GameEvents.TowerBuilt(this);
        // GameEvents.TowerBuilt(this);
        // GameEvents.OnEnemyKilled.Invoke(1);
        // Debug.Log($"towerDamage = {Damage}");
    }

    protected void FindTarget()
    {
        var objects = Physics.OverlapBox(transform.position, new Vector3(Range/2,Range/2,Range/2));
        foreach (var item in objects)
        {
            if (item.gameObject.GetComponent<Enemy>())
            {
                Debug.Log(Vector3.Distance(transform.position,item.transform.position));
                Targets.Add(item.gameObject.GetComponent<Enemy>());
                StartCoroutine(DrawLaser());
                return;
            }
        }
    }

    IEnumerator DrawLaser()
    {
        while (Targets.Count != 0 && Targets[0] != null)
        {
            Debug.DrawLine(transform.position+new Vector3(0,1f,0) ,Targets[0].transform.position,Color.red);
            yield return null;
        }
    }

    protected virtual void Attack()
    {
        currentTime -= Time.deltaTime;
        if (Targets.Count == 0 && currentTime<0)
        {
            currentTime = 0;
            return;
        }
        if (!(currentTime <= 0)) return;
        int shotsPerTick = (int) Mathf.Floor(Mathf.Abs(currentTime) / SecondsOnAttack);
        for (int i = 0; i <= shotsPerTick; i++)
        {
            SourceOfDamage newBullet = Instantiate(SourceOfDamagePrefab, transform.position,Quaternion.identity);
            newBullet.transform.position = SourcePosition.position;
            newBullet.Init(Targets,Damage,DebuffsToApply);
        }
        currentTime = SecondsOnAttack;
        

    }

    protected virtual void CheckTarget()
    {
        List<Enemy> leastTargets = new List<Enemy>();
        for (int index = 0; index < Targets.Count; index++)
        {
            Enemy target = Targets[index];
            Debug.Log(target);
            if (target != null! && Vector3.Distance(transform.position, target.transform.position) <= Range / 2)
            {
                leastTargets.Add(target);
            }
        }

        Targets = leastTargets;
        if (Targets.Count==0)
        {
            FindTarget();
        }
    }
    
    // Update is called once per frame
    protected virtual void Update()
    {
        CheckTarget();
        Attack();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(Range,1,Range));

    }
}
