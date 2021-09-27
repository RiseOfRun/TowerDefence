using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MirageOfTower : MonoBehaviour
{
    private void Update()
    {
        getPosition();
    }

    void getPosition()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out float enter);
        Vector3 point = ray.GetPoint(enter);
        Vector3Int rounded = Vector3Int.RoundToInt(point);
        transform.position = rounded;
    }
}
