using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuildPanel : MonoBehaviour
{
    [HideInInspector] public List<TowerPattern> Towers;
    public SelectTowerButton ButtonPatern;
    [FormerlySerializedAs("Space")] public GameObject BuildOptionsSpace;
    public GameObject UpgradesSpace;
    [FormerlySerializedAs("UpgradesText")] public GameObject UpgradesUI;
    public TMP_Text UpgradesText;

    // Start is called before the first frame update
    void Start()
    {
        TargetSystem.Instance.OnFreeTower += FreeTower;
        Towers = LevelController.Instance.TowersToBuild;
        foreach (var tower in Towers)
        {
            var newButton = Instantiate(ButtonPatern, BuildOptionsSpace.transform);
            newButton.Init(tower);
        }
    }

    public void OnTowerSelected(Tower t)
    {
        UpgradesSpace.SetActive(true);
        UpgradesUI.SetActive(true);
        BuildOptionsSpace.SetActive(false);

        foreach (Transform child in UpgradesSpace.transform)
        {
            Destroy(child.gameObject);
        }
        if (t.Pattern.Upgrades.Count == 0)
        {
            UpgradesText.gameObject.SetActive(false);
            return;
        }
        UpgradesText.gameObject.SetActive(true);
        foreach (var pattern in t.Pattern.Upgrades)
        {
            var newButton = Instantiate(ButtonPatern, UpgradesSpace.transform);
            newButton.Init(pattern);
        }
    }

    public void FreeTower()
    {
        UpgradesSpace.SetActive(false);
        BuildOptionsSpace.SetActive(true);
        UpgradesUI.SetActive(false);
        TargetSystem.Instance.TargetedTower = null;
    }

    // Update is called once per frame
    void Update()
    {
    }
}