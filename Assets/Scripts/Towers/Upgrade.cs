using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "TowerUpgrade")]
public class Upgrade : ScriptableObject
{
    public int Cost;
    public string Name;
    public int Lvl;
    public Tower TowerToUpgrade;
    public Image Icon;
}