using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class CameraController : MonoBehaviour
{
    public Vector3 FirstClickPosition;
    private bool inRotateMode = false;
    public float Speed = 5f;
    public float MaxHorizon;
    public GameObject Handler;

    public float Threshold = 0.1f;

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

        if (Mathf.Abs(horizontal) > Threshold)
        {
            Handler.transform.Rotate(Vector3.up, horizontal * Speed, Space.World);
        }

        Quaternion lastTransform = Handler.transform.rotation;
        if (Mathf.Abs(vertical) > Threshold)
        {
            Handler.transform.Rotate(transform.right, -vertical * Speed, Space.World);
            if (transform.position.y<0.1f)
            {
                Handler.transform.rotation = lastTransform;
            }
        }
        
        return to;
    }
}