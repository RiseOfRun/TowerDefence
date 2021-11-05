using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WaveController : MonoBehaviour
{
    public float ScoreMulti = 1;
    public float HealthMulti = 1;
    public float SpeedMulti = 1;
    public List<WaveSettings> WaveSettings = new List<WaveSettings>();

    public Enemy GetEnemy(EnemyGroup party)
    {
        Enemy newEnemy = Instantiate(party.EnemyInGroup, LevelController.Instance.UnitPool.transform);
        if (party.ModifyHealth)
        {
            newEnemy.Health = party.Health;
            newEnemy.Score = party.Score;
            newEnemy.Speed = party.Speed;
        }
        newEnemy.Init(ScoreMulti, HealthMulti, SpeedMulti, LevelController.Instance.CurrentWave - 1);
        return newEnemy;
    }
}

[Serializable]
public class WaveSettings
{
    public string Name = "Wave Number:";

    public int Size
    {
        get
        {
            return EnemyGroups.Sum(@group => @group.Size);
        }
    }
    
    public float Delay = 1f;
    public List<EnemyGroup> EnemyGroups = new List<EnemyGroup>();
}