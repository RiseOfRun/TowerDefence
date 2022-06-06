using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    [FormerlySerializedAs("UIPanel")] public GameObject UICanvas;
    [HideInInspector] public GameObject UnitPool;
    [HideInInspector] public List<Tower> Towers = new List<Tower>();
    public BuildPanel BuildPanel;
    [FormerlySerializedAs("timeToWave")] public float TimeToWave;
    public Spawner[] Spawners;
    public int PlayerStartMoney = 50;
    public int WaveCount;
    public int TimeBetweenWaves;
    public int CurrentWave = 1;
    public bool WaveInProgress;
    public List<TowerPattern> TowersToBuild;
    [FormerlySerializedAs("OnWaveEnd")] public UnityEvent EndWave = new UnityEvent();

    private WaveController waveController;
    private int enemyCount;
    private GameObject winPanel;
    private GameObject losePanel;
    private bool gameIN = true;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        UICanvas = FindObjectOfType<UICanvas>(true).gameObject;
        losePanel = UICanvas.GetComponentInChildren<LosePanel>(true).gameObject;
        winPanel = UICanvas.GetComponentInChildren<WinPanel>(true).gameObject;
        BuildPanel = UICanvas.GetComponentInChildren<BuildPanel>();
        GameEvents.OnEnemySlain.AddListener(OnUnitSlain);
        GameEvents.OnEnemyEndPath.AddListener(OnEnemyEndPath);
        waveController = GetComponentInChildren<WaveController>();
        Spawners = GetComponentsInChildren<Spawner>();
    }

    void Start()
    {
        BuildPanel.Towers = TowersToBuild;
        UICanvas.SetActive(true);
        TimeToWave = TimeBetweenWaves;
        Player.Instance.Money = PlayerStartMoney;
    }

    void OnUnitSlain(Enemy unit)
    {
        if (!WaveInProgress) return;
        enemyCount -= 1;
    }

    void OnEnemyEndPath(Enemy unit)
    {
        enemyCount -= 1;
        Player.Instance.Lives -= unit.Penalty;
        if (Player.Instance.Lives < 0)
        {
            Player.Instance.Lives = 0;
        }

        Destroy(unit.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIN) return;
        if (!WaveInProgress)
        {
            TimeToWave -= Time.deltaTime;
            if (TimeToWave <= 0)
            {
                NextWave();
            }
        }

        if (WaveInProgress)
        {
            CheckAllive();
        }

        CheckGameState();
    }

    void CheckGameState()
    {
        if (Player.Instance.Lives <= 0)
        {
            OnGameOver();
        }

        if (CurrentWave > WaveCount && !WaveInProgress)
        {
            gameIN = false;
            Invoke(nameof(OnGameWin),0.5f);
        }
    }

    void OnGameWin()
    {
        winPanel.SetActive(true);
        if (PlayerPrefs.GetInt("CompletedLevels",-1) < GameManager.CurrentLevel)
        {
            PlayerPrefs.SetInt("CompletedLevels", GameManager.CurrentLevel+1);
        }
        PlayerPrefs.SetInt("OnLevel", -1);
    }

    void OnGameOver()
    {
        gameIN = false;
        losePanel.SetActive(true);
        PlayerPrefs.SetInt("OnLevel", -1);
    }

    void CheckAllive()
    {
        if (enemyCount <= 0 && WaveInProgress)
        {
            WaveInProgress = false;
            CurrentWave += 1;
            EndWave?.Invoke();
        }
    }

    public void NextWave()
    {
        if (CurrentWave > WaveCount)
        {
            return;
        }

        TimeToWave = TimeBetweenWaves;
        WaveSettings waveSettings = waveController.WaveSettings.First();
        enemyCount = waveSettings.Size * Spawners.Length;
        WaveInProgress = true;
        foreach (var spawner in Spawners)
        {
            spawner.SpawnWave(waveSettings, waveController);
        }
    }
}