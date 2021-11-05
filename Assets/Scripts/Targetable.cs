using System;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    public float Health = 0;
    public Action<float> OnHealthChanged;
    public List<Debuff> Debuffs = new List<Debuff>();
    
    private void Awake()
    {
    }

    void Start()
    {
        
    }

    public void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void HandleDebuffs()
    {
        foreach (Debuff debuff in Debuffs)
        {
            debuff.Update();
        }

        Debuffs.RemoveAll(d => d.IsOver);
    }

    public virtual void ApplyDamage(float damage)
    {
        Health -= damage;
        if (Health<=0)
        {
            Destroy(gameObject);
        }
        OnHealthChanged?.Invoke(Health);
    }

    public virtual void OnDeath()
    {
        
    }
    
}