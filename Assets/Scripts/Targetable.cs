using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Targetable : MonoBehaviour
{
    public float Health
    {
        get => health;
        set
        {
            if (Math.Abs(value - health) < 0.01f) return;
            health = value;
            OnHealthChanged?.Invoke(health);
        }
    }
    public Action<float> OnHealthChanged;
    public List<Debuff> Debuffs = new List<Debuff>();
    [HideInInspector] public float MaxHealth;
    [SerializeField] protected float health = 0;

    private void Start()
    {
        MaxHealth = health;
    }

    public virtual void Update()
    {
        HandleDebuffs();
    }

    private void HandleDebuffs()
    {
        foreach (Debuff debuff in Debuffs)
        {
            debuff.Update();
        }

        Debuffs.RemoveAll(d => d.IsOver);
    }

    public void ApplyDamage(float damage)
    {
        if (Health <= 0)
        {
            return;
        }
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}