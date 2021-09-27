using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPanel : MonoBehaviour
{
    public List<MirageOfTower> Mirages;
    public List<Tower> TowerPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectTower(int i)
    {
        BuildManager.Instance.EnterToBuildMode(Mirages[i],TowerPrefabs[i]);
    }
}
