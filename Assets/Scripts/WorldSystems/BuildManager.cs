using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public Tower currentTower;
    public static BuildManager Instance;
    public bool InBuildMode = false;
    public MirageOfTower MiragePrefab;

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


    // Update is called once per frame
    void Update()
    {
        CheckClick();
    }

    public void CheckClick()
    {
        if (!InBuildMode) return;
        if (Input.GetMouseButtonDown(0))
        {
            InBuildMode = true;
            if (InBuildMode)
            {
                BuildMirageTower();
            }
        }
        if (Input.GetMouseButtonDown(1) && mirage !=null)
        {
            InBuildMode = false;
            Destroy(mirage.gameObject);
        }
    }

    public void BuildMirageTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo)) return;
        Square hitSquare = hitInfo.collider.gameObject.GetComponent<Square>();
        if (hitSquare == null || !hitSquare.CanBuild) return;
        if (Player.Instance.Money < currentTower.Cost) return;
        InBuildMode = false;
        Build(hitSquare.transform.position);
        Destroy(mirage.gameObject);
        hitSquare.OnBuildTower();
    }

    public void Build(Vector3 point)
    {
        Tower newTower = Instantiate(currentTower, point, Quaternion.identity);
        newTower.transform.position += new Vector3(0, 0.2f, 0);
        LevelController.Instance.Towers.Add(newTower);
        Player.Instance.Money -= currentTower.Cost;
    }

    public void EnterToBuildMode(MirageOfTower miragePref, Tower towerPref)
    {
        MiragePrefab = miragePref;
        currentTower = towerPref;
        InBuildMode = true;
        mirage = Instantiate(Instance.MiragePrefab);
    }
}