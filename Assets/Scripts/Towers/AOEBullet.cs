using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;

public class AOEBullet : Bullet
{
    public float ExplosionRange;
    [FormerlySerializedAs("Explosion")] public GameObject Effect;

    IEnumerator DoDamage(Enemy target)
    {
        for (float i = 0; i < 0.25f; i+=Time.deltaTime)
        {
            yield return null;
        }
        ApplyDebuffs(target);
        target.ApplyDamage(Damage);
    }
    public override void DoAction()
    {
        var currentTargets = Physics.OverlapSphere(transform.position, ExplosionRange,
            LayerMask.GetMask("Enemies"));
        foreach (var target in currentTargets)
        {
           
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy==null)
            {
                continue;
            }
            enemy.StartCoroutine(DoDamage(enemy));
        }

        if (Effect!=null)
        {
            GameObject exp = Instantiate(Effect,transform.position,Quaternion.identity);
            exp.transform.localScale = new Vector3(ExplosionRange*2, ExplosionRange, ExplosionRange*2); 
        }
        Destroy(gameObject);
    }
}
