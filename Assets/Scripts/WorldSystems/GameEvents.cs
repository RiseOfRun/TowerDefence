using UnityEngine.Events;

public class GameEvents
{
    
    public static readonly UnityEvent<Enemy> OnEnemyEndPath = new UnityEvent<Enemy>();
    public static readonly UnityEvent<Enemy> OnEnemySlain = new UnityEvent<Enemy>();
    public static readonly UnityEvent<Tower> OnTowerBuilt = new UnityEvent<Tower>();
    public static readonly UnityEvent<int> OnWaveStart = new UnityEvent<int>();
    public static readonly UnityEvent OnWavesEnd = new UnityEvent();

    public static void TowerBuilt(Tower t)
    {
        OnTowerBuilt?.Invoke(t);
    }

    public static void StartWave(int number)
    {
        OnWaveStart?.Invoke(number);
    }

    public static void EnemyEndPath(Enemy unit)
    {
        OnEnemyEndPath?.Invoke(unit);
    } 
    
    public static void EnemySlain(Enemy unit)
    {
        OnEnemySlain?.Invoke(unit);
    }

    public static void WavesEnd()
    {
        OnWavesEnd?.Invoke();
    }
}