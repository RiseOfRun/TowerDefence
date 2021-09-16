using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health
    {
        get => health;
        set
        {
            health = value;
            Debug.Log($"Ouch {health}");
        }
    }
    public float Speed;
    public float Score;
    public Transform[] Waypoints;
    public Queue<Vector3> Path = new Queue<Vector3>();
    [SerializeField] private int health;

    void Start()
    {
        foreach (var point in Waypoints)
        {
            Path.Enqueue(point.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Health<=0)
        {
            Destroy(gameObject);
            return;
        }

        if (Path.Count==0)
        {
            return;
        }
        Vector3 point = Path.Peek();
        transform.position = Vector3.MoveTowards(transform.position,point,Time.deltaTime*Speed);
        if (transform.position==point)
        {
            Path.Dequeue();
        }

        
    }
}
