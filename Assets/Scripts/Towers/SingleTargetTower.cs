using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SingleTargetTower : Tower
{
    protected override void CheckTarget()
    {
        Targets.Clear();
        if (CanForceTarget)
        {
            var target = TargetSystem.Instance.EnemyTargets.FirstOrDefault(x => x is Enemy && x != null
                && x.enabled && Vector3.Distance(transform.position, x.transform.position) <= Pattern.Range);
            if (target!=null)
            {
                Targets.Add(target);
                return;
            }
        }

        int count = Physics.OverlapSphereNonAlloc(transform.position, Pattern.Range, TargetBuffer,
            LayerMask.GetMask("Enemies"));
      
        float maxPath = -1;
        Enemy bestEnemy = null;
        for (int i = 0; i < count; i++)
        {
            Enemy currentEnemy = TargetBuffer[i].GetComponent<Enemy>();
            if (currentEnemy!=null && currentEnemy.enabled && currentEnemy.PercentComplete > maxPath)
            {
                bestEnemy = currentEnemy;
                maxPath = currentEnemy.PercentComplete;
            }
        }

        if(bestEnemy!=null)
        {
            Targets.Add(bestEnemy);
            return;
        }

        if (CanForceTarget)
        {
            var dObject = TargetSystem.Instance.EnemyTargets.FirstOrDefault(
                x => x is DestroyableObject && x != null &&
                     Vector3.Distance(transform.position, x.transform.position) < Pattern.Range);
            if (dObject!=null)
            {
                Targets.Add(dObject);
            }
        }
        
    }
}
