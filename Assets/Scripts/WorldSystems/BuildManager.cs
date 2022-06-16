using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildManager : MonoBehaviour
{
    public TowerPattern currentTower;
    public static BuildManager Instance;
    public bool InBuildMode = false;
    [FormerlySerializedAs("BuildPanel")] public BuildPanel BuildOptionsPanel;
    [HideInInspector] public MirageOfTower MiragePrefab;
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
    }
    
    public void TryBuildTower()
    {
        var position = Mirage.transform.position;
        Ray ray = new Ray(new Vector3(position.x,100,position.z), Vector3.down);
        ExitFromBuildMod();
        if (!Physics.Raycast(ray, out RaycastHit hitInfo,
            float.MaxValue, LayerMask.GetMask("Ground")))
        {
            return;
        }

        Square hitSquare = hitInfo.collider.gameObject.GetComponentInParent<Square>();
        if (hitSquare == null || !hitSquare.CanBuild)
        {
            return;
        }
        Build(hitSquare,currentTower);
    }

    public void Build(Square place, TowerPattern tower)
    {
        if (Player.Instance.Money < tower.Cost) return;
        Tower newTower = tower.SpawnTower(place);
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
        BuildOptionsPanel.FreeTower();
        Build(place,p);
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

    public void ExitFromBuildMod()
    {
        InBuildMode = false;
        Destroy(Mirage.gameObject);
    }
}