using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BuildManager : MonoBehaviour
{
    public Tower currentTower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckClick();
    }

    public void CheckClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out float enter);
        Vector3 point = ray.GetPoint(enter);
        Vector3Int rounded = Vector3Int.RoundToInt(point);
        Build(rounded);
    }

    public void Build(Vector3 point)
    {
        Instantiate(currentTower, point, Quaternion.identity);
    }
}
