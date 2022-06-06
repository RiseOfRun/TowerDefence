using UnityEngine.Events;

public class GameEvents
{
    
    public static readonly UnityEvent<Enemy> OnEnemyEndPath = new UnityEvent<Enemy>();
    public static readonly UnityEvent<Enemy> OnEnemySlain = new UnityEvent<Enemy>();
    public static readonly UnityEvent<Tower> OnTowerBuilt = new UnityEvent<Tower>();

    public static void TowerBuilt(Tower t)
    {
        OnTowerBuilt?.Invoke(t);
    }

    public static void EnemyEndPath(Enemy unit)
    {
        OnEnemyEndPath?.Invoke(unit);
    } 
    
    public static void EnemySlain(Enemy unit)
    {
        OnEnemySlain?.Invoke(unit);
    }
}