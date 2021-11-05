using System.Collections.Generic;
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
    public bool Compose = false;    
    public SourceOfDamage DamageSource;
    public float Damage;
    public float APS;
    public int Cost;
    public List<Debuff> Debuffs = new List<Debuff>();
    public List<TowerPattern> Upgrades = new List<TowerPattern>();
    
    public Tower SpawnTower(Square square)
    {
        Tower newTower =  Instantiate(TowerPref, square.transform);
        newTower.Upgrades = Upgrades;
        newTower.transform.position += new Vector3(0, 0.2f, 0);
        LevelController.Instance.Towers.Add(newTower);
        if (Compose)
        {
            newTower.SourceOfDamagePrefab = DamageSource;
            newTower.DebuffsToApply = Debuffs;
            newTower.Damage = Damage;
            newTower.AttackPerSecond = APS;
            newTower.Cost = Cost;
        }
        return newTower;
    }
}
