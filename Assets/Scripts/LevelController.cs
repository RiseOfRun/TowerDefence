using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    [FormerlySerializedAs("UIPanel")] public GameObject UICanvas;
    [HideInInspector]public GameObject UnitPool;
    [HideInInspector]public List<Tower> Towers = new List<Tower>();
    public BuildPanel BuildPanel;
    [FormerlySerializedAs("timeToWave")] public float TimeToWave;
    public Spawner[] Spawners;
    public int WaveCount;
    public int TimeBetweenWaves;
    public int CurrentWave;
    public bool WaveInProgress;
    public List<TowerPattern> TowersToBuild;
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
        } else if(Instance == this)
        {
            Destroy(gameObject);
        }
        losePanel = UICanvas.GetComponentInChildren<LosePanel>(true).gameObject;
        winPanel = UICanvas.GetComponentInChildren<WinPanel>(includeInactive:true).gameObject;
        BuildPanel = UICanvas.GetComponentInChildren<BuildPanel>();
        GameEvents.OnEnemySlain.AddListener(OnUnitSlain);
        GameEvents.OnEnemyEndPath.AddListener(OnEnemyEndPath);
        waveController = GetComponentInChildren<WaveController>();
        Spawners = GetComponentsInChildren<Spawner>();
        
    }

    void Start()
    {
        BuildPanel.Towers = TowersToBuild;
        TimeToWave = TimeBetweenWaves;
        CurrentWave = 1;
        
    }
    
    //ToDO Save/Load system

    void OnUnitSlain(Enemy unit)
    {
        if (!WaveInProgress) return;
        enemyCount -= 1;
    }

    void OnEnemyEndPath(Enemy unit)
    {
        enemyCount -= 1;
        Player.Instance.Lives -= unit.Penalty;
        if (Player.Instance.Lives<0)
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
            if (TimeToWave<=0)
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
        if (CurrentWave>WaveCount && !WaveInProgress)
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
        winPanel.SetActive(true);
        gameIN = false;
    }void OnGameOver()
    {
        losePanel.SetActive(true);
        gameIN = false;
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
        if (CurrentWave>WaveCount)
        {
            return;
        }
        TimeToWave = TimeBetweenWaves;
        WaveSettings waveSettings = waveController.WaveSettings.First();
        enemyCount = waveSettings.Size*Spawners.Length;
        WaveInProgress = true;
        foreach (var spawner in Spawners)
        {
            spawner.SpawnWave(waveSettings, waveController);
        }
        GameEvents.StartWave(CurrentWave);
    }
}