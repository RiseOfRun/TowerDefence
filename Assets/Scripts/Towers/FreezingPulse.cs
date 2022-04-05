using System;
using UnityEngine;
[Serializable]
public class FreezingPulse : SourceOfDamage
{
    // Start is called before the first frame update
    void Start()
    {
        var t = GetComponentInParent<Tower>().Pattern;
        transform.localScale = new Vector3(t.Range / 2f,1,t.Range/2f);
        foreach (var target in Targets)
        {
            ApplyDebuffs(target);
            target.ApplyDamage(Damage);
        }
        Invoke("EndEffect",1);
    }

    void EndEffect()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
