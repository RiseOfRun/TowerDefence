using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    [HideInInspector]public GameObject winPanel;
    [HideInInspector]public GameObject losePanel;
    [HideInInspector]public GameObject UnitPool;
    [HideInInspector]public List<Tower> Towers = new List<Tower>();
    [HideInInspector][FormerlySerializedAs("Panel")] public BuildPanel BuuldPanel;
    [HideInInspector][FormerlySerializedAs("CoinGain")] public ScoreAlert ScoreTablet;
    public float timeToWave;
    public Field GameField;
    public Spawner[] Spawners;
    public GameObject Finish;
    public int WaveCount;
    public int TimeBetweenWaves;
    public bool newStart = true;
    public int CurrentWave;
    public bool WaveInProgress;
    public List<TowerPattern> TowersToBuild;
    private GameObject UIPanel;
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
        UIPanel = GetComponentInChildren<levelUI>().gameObject;
        BuuldPanel = GetComponentInChildren<BuildPanel>();
        GameEvents.OnEnemySlain.AddListener(OnUnitSlain);
        GameEvents.OnEnemyEndPath.AddListener(OnEnemyEndPath);
        waveController = GetComponentInChildren<WaveController>();
        Spawners = GetComponentsInChildren<Spawner>();
        
    }

    void Start()
    {
        BuuldPanel.Towers = TowersToBuild;
        timeToWave = TimeBetweenWaves;
        CurrentWave = 1;
        
    }

    void OnUnitSlain(Enemy unit)
    {
        if (!WaveInProgress) return;
        var tablet = Instantiate(ScoreTablet, UIPanel.transform);
        tablet.Origin = unit.transform.position;
        tablet.Count = unit.Score;
        enemyCount -= 1;
        Player.Instance.Money += unit.Score;
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
        if (!WaveInProgress)
        {
            timeToWave -= Time.deltaTime;
            if (timeToWave<=0)
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
        UIPanel.SetActive(false);
        winPanel.SetActive(true);
    }void OnGameOver()
    {
        UIPanel.SetActive(false);
        losePanel.SetActive(true);
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
        timeToWave = TimeBetweenWaves;
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