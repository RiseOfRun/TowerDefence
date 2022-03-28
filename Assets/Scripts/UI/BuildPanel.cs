using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildPanel : MonoBehaviour
{
    [HideInInspector] public List<TowerPattern> Towers;
    public SelectTowerButton ButtonPatern;
    [FormerlySerializedAs("Space")] public GameObject BuildOptionsSpace;
    public GameObject UpgradesSpace;
    [FormerlySerializedAs("UpgradesText")] public GameObject UpgradesUI;
    
    // Start is called before the first frame update
    void Start()
    {
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
        
        foreach (var pattern in t.Pattern.Upgrades)
        {
            var newButton = Instantiate(ButtonPatern, UpgradesSpace.transform);
            newButton.Init(pattern);
        }
    }

    public void OnFreeTower()
    {
        UpgradesSpace.SetActive(false);
        BuildOptionsSpace.SetActive(true);
        UpgradesUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
