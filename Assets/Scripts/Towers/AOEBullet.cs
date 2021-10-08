using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AOEBullet : Bullet
{
    public float ExplosionRange;
    public GameObject Explosion;
    void Start()
    {
        
    }

    IEnumerator DoDamage(Enemy target)
    {
        
        for (float i = 0; i < 0.25f; i+=Time.deltaTime)
        {
            yield return null;
        }
        target.ApplyDamage(Damage);
    }
    public override void DoAction()
    {
        var currentTargets = Physics.OverlapSphere(transform.position, ExplosionRange/2,
            LayerMask.GetMask("Enemies"));
        foreach (var target in currentTargets)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.StartCoroutine(DoDamage(enemy));
        }

        if (Explosion!=null)
        {
            GameObject exp = Instantiate(Explosion,transform.position,Quaternion.identity);
            exp.transform.localScale = new Vector3(ExplosionRange, ExplosionRange, ExplosionRange); 
        }
        Destroy(gameObject);
    }
}
