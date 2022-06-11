using System.Collections.Generic;
using TMPro;
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

    public string Description
    {
        get
        {
            string parcedDescription = description;
            parcedDescription = parcedDescription.Replace("%d", Damage.ToString("0.00"));
            parcedDescription = parcedDescription.Replace("%r", Range.ToString("0.00"));
            parcedDescription = parcedDescription.Replace("%a", APS.ToString("0.00"));
            return parcedDescription;
        }
    }
    public bool IsUpgrade = false;
    public SourceOfDamage DamageSource;
    public float Damage;
    public float APS;
    public int Cost;
    public float Range;
    public List<Debuff> Debuffs = new List<Debuff>();
    public List<TowerPattern> Upgrades = new List<TowerPattern>();

    [SerializeField][TextArea] private string description;

    public Tower SpawnTower(Square square)
    {
        Tower newTower =  Instantiate(TowerPref, square.transform);
        newTower.transform.position += new Vector3(0, 0.2f, 0);
        LevelController.Instance.Towers.Add(newTower);
        newTower.Pattern = this;
        return newTower;
    }
}

