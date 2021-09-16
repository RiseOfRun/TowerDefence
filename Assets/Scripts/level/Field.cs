using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Square BlankPrefab;
    public int Rows, Cols;

    private Square[,] field;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Collider[] overlapSphere = Physics.OverlapSphere(tower.position, 10);
        //
        // Plane plane = new Plane(Vector3.up, Vector3.zero);
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //
        // plane.Raycast(ray, out float enter);
        // Vector3 vector3 = ray.GetPoint());
    }
}
