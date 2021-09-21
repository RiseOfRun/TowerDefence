using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float Range;
    public int Damage=200;
    public int Cost;
    
    private Enemy target;
    public Bullet BulletPrefab;
    public float AttackPerSecond = 1f;
    private float currentTime = 0;
    private float secondsOnAttack;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        secondsOnAttack = 1 / AttackPerSecond;
        GameEvents.TowerBuilt(this);
        // GameEvents.TowerBuilt(this);
        // GameEvents.OnEnemyKilled.Invoke(1);
        // Debug.Log($"towerDamage = {Damage}");
    }

    void FindTarget()
    {
        var objects = Physics.OverlapBox(transform.position, new Vector3(Range/2,Range/2,Range/2));
        foreach (var item in objects)
        {
            if (item.gameObject.GetComponent<Enemy>())
            {
                Debug.Log(Vector3.Distance(transform.position,item.transform.position));
                target = item.gameObject.GetComponent<Enemy>();
                StartCoroutine(DrawLazer());
                return;
            }
        }
    }

    IEnumerator DrawLazer()
    {
        while (target!=null)
        {
            Debug.DrawLine(transform.position+new Vector3(0,1f,0) ,target.transform.position,Color.red);
            yield return null;
        }
    }

    public void Shot()
    {
        currentTime -= Time.deltaTime;
        if (target==null&&currentTime<0)
        {
            currentTime = 0;
            return;
        }
        if (!(currentTime <= 0)) return;
        int shotsPerTick = (int) Mathf.Floor(Mathf.Abs(currentTime) / secondsOnAttack);
        for (int i = 0; i <= shotsPerTick; i++)
        {
            Bullet newBullet = Instantiate(BulletPrefab, transform.position,Quaternion.identity);
            newBullet.Init(Damage,target);
        }
        currentTime = secondsOnAttack;
        

    }

    void CheckTarget()
    {
        if (target == null || Vector3.Distance(transform.position, target.transform.position)>=Range/2)
        {
            target = null;
            FindTarget();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckTarget();
        Shot();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(Range,1,Range));

    }
}
