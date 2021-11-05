using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public TowerPattern currentTower;
    public static BuildManager Instance;
    public bool InBuildMode = false;
    public MirageOfTower MiragePrefab;
    public BuildPanel BuildPanel;
    public ParticleSystem OnBuildParticle;
    private MirageOfTower mirage;

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
        CheckClick();
    }

    public void CheckClick()
    {
        if (!InBuildMode) return;
        if (Input.GetMouseButtonDown(0))
        {
            BuildTower();
        }
        
        if (Input.GetMouseButtonDown(1) && mirage !=null)
        {
            InBuildMode = false;
            Destroy(mirage.gameObject);
        }
    }
    
    public void BuildTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo,float.MaxValue)) return;
        Square hitSquare = hitInfo.collider.gameObject.GetComponent<Square>();
        if (hitSquare == null || !hitSquare.CanBuild) return;
        Build(hitSquare,currentTower);
        Destroy(mirage.gameObject);
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

    public void EnterToBuildMode(MirageOfTower miragePref, TowerPattern towerPref)
    {
        MiragePrefab = miragePref;
        currentTower = towerPref;
        InBuildMode = true;
        if (mirage!=null)
        {
            Destroy(mirage.gameObject);
        }
        mirage = Instantiate(Instance.MiragePrefab);
    }
}