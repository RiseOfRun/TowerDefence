using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Pattern")]
public class EnemyPattern : ScriptableObject
{
    public Enemy Prefab;
    public bool Modify = false;
    public float Health;
    public float Speed;
    public int Score;


    private void Awake()
    {
        Health = Prefab.InitHealth;
        Speed = Prefab.InitSpeed;
        Score = Prefab.InitScore;
    }

    public Enemy SpawnEnemy()
    {
        Enemy newEnemy = Instantiate(Prefab);
        newEnemy.Init(Score,Health,Speed);
        return newEnemy;
    }

    
}
