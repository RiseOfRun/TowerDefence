using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public Vector3 FirstClickPosition;
    private bool inRotateMode = false;
    public float Speed = 5f;
    public GameObject Handler;

    public float Trashold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ControlRotation();
    }

    void ControlRotation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            FirstClickPosition = Input.mousePosition;
            inRotateMode = true;
        }

        if (Input.GetMouseButton(1) && inRotateMode)
        {
            Vector3 NextPosition = Input.mousePosition;
            FirstClickPosition = Rotate(NextPosition);
            Debug.Log($"power: {FirstClickPosition} {NextPosition}");
        }

        if (Input.GetMouseButtonUp(1))
        {
            inRotateMode = false;
        }
    }

    Vector3 Rotate(Vector3 to)
    {
        float horizontal = to.x - FirstClickPosition.x;
        float vertical = to.y - FirstClickPosition.y;

        if (Mathf.Abs(horizontal) > Trashold)
        {
            Handler.transform.Rotate(Vector3.up, horizontal * Speed, Space.World);
        }

        if (Mathf.Abs(vertical) > Trashold)
        {
            Handler.transform.Rotate(transform.right, -vertical * Speed, Space.World);
        }
        
        return to;
    }
}