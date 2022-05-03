using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuildPanel : MonoBehaviour
{
    [HideInInspector] public List<TowerPattern> Towers;
    public SelectTowerButton ButtonPattern;

    [Header("BuildOptions")]
    public GameObject BuildOptions;
    public GameObject BuildOptionsSpace;
    [Header("Upgrades")]
    public GameObject UpgradesSpace;
    [FormerlySerializedAs("UpgradesText")] public GameObject UpgradesUI;
    public TMP_Text UpgradesText;
    [Header("Description")]
    public GameObject DescriptionSpace;
    public TMP_Text DescriptionName;
    public TMP_Text TowerDescription;

    // Start is called before the first frame update
    void Start()
    {
        TargetSystem.Instance.OnFreeTower += FreeTower;
        Towers = LevelController.Instance.TowersToBuild;
        foreach (var tower in Towers)
        {
            var newButton = Instantiate(ButtonPattern, BuildOptionsSpace.transform);
            newButton.Init(tower);
        }
    }

    public void OnTowerSelected(Tower t)
    {
        UpgradesSpace.SetActive(true);
        UpgradesUI.SetActive(true);
        BuildOptions.SetActive(false);

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
            var newButton = Instantiate(ButtonPattern, UpgradesSpace.transform);
            newButton.Init(pattern);
        }
        ShowDescription(t.Pattern);
    }

    public void FreeTower()
    {
        UpgradesSpace.SetActive(false);
        BuildOptions.SetActive(true);
        UpgradesUI.SetActive(false);
        TargetSystem.Instance.TargetedTower = null;
        HideDescription();
    }

    public void ShowDescription(TowerPattern tower)
    {
        DescriptionName.text = tower.Name + ":";
        TowerDescription.text = tower.Description;
        DescriptionSpace.SetActive(true);

    }

    public void HideDescription()
    {
        DescriptionSpace.SetActive(false);
    }
    
    public 
    // Update is called once per frame
    void Update()
    {
    }
}