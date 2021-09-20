using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public Field GameField;
    public Spawner[] Spawners;
    public GameObject Finish;
    public bool DynamicMode = true;
    public int WaveCount;
    public bool newStart = true;
    public GameObject UnitPool;
    public int currentWave;

    private WaveController waveController;
    private bool waveInProgress = false;
    private int enemyCount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.OnEnemySlain.AddListener(OnUnitSlain);
        currentWave = 1;
        waveController = GetComponentInChildren<WaveController>();
        if (waveController != null)
        {
            DynamicMode = true;
        }

        Spawners = GetComponentsInChildren<Spawner>();
    }

    void OnUnitSlain(Enemy unit)
    {
        if (!waveInProgress) return;
        enemyCount -= 1;
        Player.Instance.Money += unit.Score;
    }

    // Update is called once per frame
    void Update()
    {
        if (waveInProgress)
        {
            CheckAllive();
        }
    }

    void CheckAllive()
    {
        Debug.Log($"Units on map : {UnitPool.transform.childCount}");
    }

    public void NextWave()
    {
        WaveSettings waveSettings = waveController.WaveSettings.First();
        foreach (var enemyGroup in waveSettings.EnemyGroups)
        {
            foreach (var enemy in enemyGroup.EnemiesInGroup)
            {
                enemy.Init(waveController.ScoreMulti, waveController.HealthMulti, waveController.SpeedMulti, 1);
            }
        }

        foreach (var spawner in Spawners)
        {
            spawner.SpawnWave(waveSettings, waveController);
        }

        waveInProgress = true;
        GameEvents.StartWave(currentWave);
        enemyCount = waveSettings.Size;

    }
}