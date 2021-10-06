using System.Collections.Generic;
using UnityEngine;

public class BuildPanel : MonoBehaviour
{
    public List<TowerPattern> Towers;
    public SelectTowerButton ButtonPatern;
    public GameObject Space;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var tower in Towers)
        {
            var newButton = Instantiate(ButtonPatern, Space.transform);
            newButton.Init(tower);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
