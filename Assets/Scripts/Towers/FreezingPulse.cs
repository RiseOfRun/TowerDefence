using System;
using UnityEngine;
[Serializable]
public class FreezingPulse : SourceOfDamage
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"I have {Targets.Count} target");
        
        foreach (var target in Targets)
        {
            ApplyDebuffs(target);
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
