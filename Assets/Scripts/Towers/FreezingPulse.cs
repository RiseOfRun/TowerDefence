using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingPulse : SourceOfDamage
{
    public GameObject DebuffEffect;
    public float SlowEffect;
    public float Duration;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"I have {Targets.Count} target");
        foreach (var target in Targets)
        {
            var debuff = ScriptableObject.CreateInstance<ChillDebuff>();
            debuff.Init(target,SlowEffect,DebuffEffect,Duration);
        }
        Destroy(gameObject);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
