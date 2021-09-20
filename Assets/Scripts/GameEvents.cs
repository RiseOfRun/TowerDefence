using System;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents
{
    public static event Action<int> OnEnemyKilled;
    public static readonly UnityEvent<Tower> OnTowerBuilt = new UnityEvent<Tower>();

    public static void TowerBuilt(Tower t)
    {
        OnTowerBuilt?.Invoke(t);
    }
}