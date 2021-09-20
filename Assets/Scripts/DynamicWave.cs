using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicWave : WaveController
{
    public float ScoreMulti;
    public float HealthMulti;
    public float SpeedMulti;

    private WaveSettings myWave => WaveSettings.First();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupWave(int level = 1)
    {
        foreach (var enemyGroup in myWave.EnemyGroups)
        {
            foreach (var enemy in enemyGroup.EnemiesInGroup)
            {
                enemy.Init(ScoreMulti,HealthMulti,SpeedMulti,level);
            }
        }
    }
}
