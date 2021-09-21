using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public bool Blank = true;
    public bool CanBuild = false;
    public GameObject BlankMesh;
    public GameObject TowerMesh;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBuildTower()
    {
        if (!CanBuild) return;
        CanBuild = false;
        BlankMesh.SetActive(false);
        TowerMesh.SetActive(true);
    }
}
