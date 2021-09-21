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
    public int CurrentWave;
    [SerializeField] private bool waveInProgress;
    public bool WaveInProgress
    {
        get => waveInProgress;
        set
        {
            Debug.Log("Set to "+value);
            waveInProgress = value;
        }
    }

    private WaveController waveController;
    private int enemyCount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.OnEnemySlain.AddListener(OnUnitSlain);
        CurrentWave = 1;
        waveController = GetComponentInChildren<WaveController>();
        if (waveController != null)
        {
            DynamicMode = true;
        }

        Spawners = GetComponentsInChildren<Spawner>();
    }

    void OnUnitSlain(Enemy unit)
    {
        if (!WaveInProgress) return;
        enemyCount -= 1;
        Player.Instance.Money += unit.Score;
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveInProgress)
        {
            CheckAllive();
        }
        CheckGameState();
    }

    void CheckGameState()
    {
        if (CurrentWave==WaveCount && !WaveInProgress)
        {
            OnGameWin();
        }

        if (Player.Instance.Lives<=0)
        {
            OnGameOver();
        }
    }

    void OnGameWin()
    {
        
    }void OnGameOver()
    {
        
    }
    void CheckAllive()
    {
        if (enemyCount<=0 && WaveInProgress)
        {
            WaveInProgress = false;
            CurrentWave += 1;
        }
    }

    public void NextWave()
    {        
        WaveSettings waveSettings = waveController.WaveSettings.First();
        enemyCount = waveSettings.Size;
        WaveInProgress = true;
        foreach (var enemyGroup in waveSettings.EnemyGroups)
        {
            foreach (var enemy in enemyGroup.EnemiesInGroup)
            {
                enemy.Init(waveController.ScoreMulti, waveController.HealthMulti, 
                    waveController.SpeedMulti, CurrentWave-1);
            }
        }
        
        foreach (var spawner in Spawners)
        {
            spawner.SpawnWave(waveSettings, waveController);
        }
        GameEvents.StartWave(CurrentWave);
    }
}