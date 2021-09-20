using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveController : MonoBehaviour
{
    public float ScoreMulti = 1;
    public float HealthMulti = 1;
    public float SpeedMulti = 1;
    public List<WaveSettings> WaveSettings = new List<WaveSettings>();
}

[Serializable]
public class WaveSettings
{
    public int Size;
    public float Delay = 1f;
    public List<EnemyGroup> EnemyGroups = new List<EnemyGroup>();
}