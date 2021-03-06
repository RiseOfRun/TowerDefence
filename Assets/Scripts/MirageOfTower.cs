using System;
using UnityEngine;

public class MirageOfTower : MonoBehaviour
{
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        Plane plane = new Plane(Vector3.up, new Vector3(0,0.2f,0));
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out float enter);
        Vector3 point = ray.GetPoint(enter);
        Vector3Int rounded = Vector3Int.RoundToInt(point);
        transform.position = rounded;
    }
}
