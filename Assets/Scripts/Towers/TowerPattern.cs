using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TowerPattern")]
public class TowerPattern : ScriptableObject
{
    public Tower TowerPref;
    public MirageOfTower Mirage;
    public Sprite Icon;
    public string Name;
    public bool IsUpgrade = false;
    public SourceOfDamage DamageSource;
    public float Damage;
    public float APS;
    public int Cost;
    public float Range;
    public List<Debuff> Debuffs = new List<Debuff>();
    public List<TowerPattern> Upgrades = new List<TowerPattern>();
    
    public Tower SpawnTower(Square square)
    {
        Tower newTower =  Instantiate(TowerPref, square.transform);
        newTower.transform.position += new Vector3(0, 0.2f, 0);
        LevelController.Instance.Towers.Add(newTower);
        newTower.Pattern = this;
        return newTower;
    }
}

