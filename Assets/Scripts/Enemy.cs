using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public float Health;
    public float Speed;
    public int Score;
    
    public Transform[] Waypoints;
    public Queue<Vector3> Path = new Queue<Vector3>();
    [SerializeField] private float InitHealth;
    [SerializeField] private int InitScore;
    [SerializeField] private float InitSpeed;
    private UnityEvent ev;
    void Start()
    {
        Health = InitHealth;
        Score = InitScore;
        Speed = InitSpeed;
        
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
            GameEvents.EnemyEndPath(this);
            Destroy(gameObject);
            return;
        }
        Vector3 point = Path.Peek();
        transform.position = Vector3.MoveTowards(transform.position,point,Time.deltaTime*Speed);
        if (transform.position==point)
        {
            Path.Dequeue();
        }
    }

    public void Init(float scoreMulti, float healthMulti, float speedMulti=1, int level = 1)
    {
        Health += InitHealth * healthMulti * level;
        Speed += InitSpeed*speedMulti*level;
        Score += (int) (InitScore * scoreMulti * level);
    }
    

    public void ApplyDamage(int damage)
    {
        Health -= damage;
        if (Health<=0)
        {
            GameEvents.EnemySlain(this);
        }
    }
}
