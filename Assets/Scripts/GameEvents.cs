using System;

public class GameEvents
{
    public static event Action<Enemy> OnEnemyKilled;
    public static event Action<Tower> OnTowerBuilt;

    public static void TowerBuilt(Tower tower)
    {
        OnTowerBuilt?.Invoke(tower);
    }
}