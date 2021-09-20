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
        RayCastCheckOnClick();
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

    public void RayCastCheckOnClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo)) return;
        Square hitSquare = hitInfo.collider.gameObject.GetComponent<Square>();
        if (hitSquare == null || !hitSquare.CanBuild) return;
        Build(hitSquare.transform.position);
        hitSquare.OnBuildTower();



    }

    public void Build(Vector3 point)
    {
        Instantiate(currentTower, point, Quaternion.identity);
    }
}
