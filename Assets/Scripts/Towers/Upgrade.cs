using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Serialization;

public class Upgrade : MonoBehaviour
{
    public int Cost;
    public string Name;
    public int UnlockOn;
    public int Lvl = 1;
    public float Damage;
    public float Range;
    public float AttackPerTick;
    public GameObject Icon;
    
    public Tower TowerToUpgrade;
    public SourceOfDamage NewSource;
    
    public void Perform()
    {
        TowerToUpgrade.Cost += Cost*Lvl;
        TowerToUpgrade.Damage += Damage*Lvl;
        TowerToUpgrade.Range += Range*Lvl;
        if (NewSource!=null)
        {
            TowerToUpgrade.SourceOfDamagePrefab = NewSource;
        }
        
    }
    
}