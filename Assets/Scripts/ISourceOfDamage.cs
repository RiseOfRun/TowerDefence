using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public abstract class SourceOfDamage : MonoBehaviour
    {
        
        public List<Enemy> Targets = new List<Enemy>();
        public float Damage = 0;
        public virtual void Init()
        {
        }

        public virtual void Init(List<Enemy> targets, float damage)
        {
            Targets = targets;
            Damage = damage;
        }
    }