using UnityEngine;

public class Square : MonoBehaviour
{
    public bool Blank = true;
    public bool CanBuild = false;
    public GameObject BlankMesh;
    public GameObject TowerMesh;
    

    public void OnBuildTower()
    {
        if (!CanBuild) return;
        CanBuild = false;
        BlankMesh.SetActive(false);
        TowerMesh.SetActive(true);
    }
}
