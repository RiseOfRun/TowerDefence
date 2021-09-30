using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    public GameObject UIPanel;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public Field GameField;
    public Spawner[] Spawners;
    public GameObject Finish;
    public bool DynamicMode = true;
    public int WaveCount;
    public bool newStart = true;
    public GameObject UnitPool;
    public int CurrentWave;
    public bool WaveInProgress;

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
        GameEvents.OnEnemyEndPath.AddListener(OnEnemyEndPath);
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

    void OnEnemyEndPath(Enemy unit)
    {
        if (!WaveInProgress) return;
        enemyCount -= 1;
        Player.Instance.Lives -= unit.Penalty;
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
        UIPanel.SetActive(false);
        WinPanel.SetActive(true);
    }void OnGameOver()
    {
        UIPanel.SetActive(false);
        LosePanel.SetActive(true);
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

        foreach (var spawner in Spawners)
        {
            spawner.SpawnWave(waveSettings, waveController);
        }
        GameEvents.StartWave(CurrentWave);
    }
}