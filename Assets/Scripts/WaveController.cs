using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WaveController : MonoBehaviour
{
    public int TotalWaveCount;
    public AnimationCurve HealthCurve;
    public AnimationCurve SpeedCureve;
    public AnimationCurve ScoreCurve;

    public List<WaveSettings> WaveSettings = new List<WaveSettings>();

    public Enemy GetEnemy(EnemyGroup party,Vector3 position)
    {
        Enemy newEnemy = Instantiate(party.EnemyInGroup, position,Quaternion.identity,LevelController.Instance.UnitPool.transform);
        newEnemy.Health = party.BaseHealth * HealthCurve.Evaluate(LevelController.Instance.CurrentWave);
        newEnemy.Speed = party.BaseSpeed * SpeedCureve.Evaluate(LevelController.Instance.CurrentWave);
        newEnemy.Score = party.BaseScore * ScoreCurve.Evaluate(LevelController.Instance.CurrentWave);
        return newEnemy;
    }
}

[Serializable]
public class WaveSettings
{
    public string Name = "Wave Number:";

    public int Size
    {
        get { return EnemyGroups.Sum(@group => @group.Size); }
    }

    public float Delay = 1f;
    public List<EnemyGroup> EnemyGroups = new List<EnemyGroup>();
}