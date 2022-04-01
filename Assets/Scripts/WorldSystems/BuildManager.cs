using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildManager : MonoBehaviour
{
    public TowerPattern currentTower;
    public static BuildManager Instance;
    public bool InBuildMode = false;
    [HideInInspector] public MirageOfTower MiragePrefab;
    [HideInInspector]public BuildPanel BuildPanel;
    public ParticleSystem OnBuildParticle;
    [HideInInspector][FormerlySerializedAs("mirage")] public MirageOfTower Mirage;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        BuildPanel = FindObjectOfType<BuildPanel>();
    }


    // Update is called once per frame
    void LateUpdate()
    {
        
    }
    
    
    public void BuildTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo,float.MaxValue)) return;
        Square hitSquare = hitInfo.collider.gameObject.GetComponent<Square>();
        if (hitSquare == null || !hitSquare.CanBuild) return;
        Build(hitSquare,currentTower);
        Destroy(Mirage.gameObject);
        InBuildMode = false;
    }

    public void Build(Square place, TowerPattern tower)
    {
        if (Player.Instance.Money < tower.Cost) return;
        Tower newTower = tower.SpawnTower(place);
        LevelController.Instance.Towers.Add(newTower);
        if (OnBuildParticle!=null)
        {
            Instantiate(OnBuildParticle, newTower.transform);
        }
        Player.Instance.Money -= tower.Cost;
        place.OnBuildTower();

    }

    public void UpgradeTower(TowerPattern p)
    {
        Tower targetTower = TargetSystem.Instance.TargetedTower;
        Square place = targetTower.transform.parent.GetComponent<Square>();
        Destroy(targetTower.gameObject);
        Instance.BuildPanel.OnFreeTower();
        Instance.Build(place,p);
    }

    public void EnterToBuildMode(MirageOfTower miragePref, TowerPattern towerPref)
    {
        MiragePrefab = miragePref;
        currentTower = towerPref;
        InBuildMode = true;
        if (Mirage!=null)
        {
            Destroy(Mirage.gameObject);
        }
        Mirage = Instantiate(Instance.MiragePrefab);
    }
}