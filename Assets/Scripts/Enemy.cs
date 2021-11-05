using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Targetable
{
    public float Speed = 0;
    public int Score = 0;
    public int Penalty = 1;
    public Transform[] Waypoints;
    public Queue<Vector3> Path = new Queue<Vector3>();
    public float InitHealth;
    public int InitScore;
    public float InitSpeed;
    public float PercentComplete => passedDistance / fullDistance;

    private float fullDistance=0;
    private float passedDistance=0;

    void Awake()
    {
        Health = InitHealth;
        Score = InitScore;
        Speed = InitSpeed;
    }
    void Start()
    {
        Vector3 currentPosition = transform.position;
        foreach (var point in Waypoints)
        {
            Path.Enqueue(point.position);
            fullDistance += Vector3.Distance(currentPosition, point.position);
            currentPosition = point.position;
        }
    }
    void Update()
    {
        if (Health <= 0)
        {
            return;
        }

        if (Path.Count == 0)
        {
            GameEvents.EnemyEndPath(this);
            //Destroy(gameObject);
            return;
        }
        HandleDebuffs();

        Vector3 point = Path.Peek();
        Vector3 nextPos = Vector3.MoveTowards(transform.position, point, Time.deltaTime * Speed);
        passedDistance += Vector3.Distance(nextPos, transform.position);
        transform.position = nextPos;
        if (transform.position == point)
        {
            Path.Dequeue();
        }
    }
    
    public void Init(float scoreMulti, float healthMulti, float speedMulti = 1, int level = 1)
    {
        Health += InitHealth * healthMulti * level;
        Speed += InitSpeed * speedMulti * level;
        Score += (int) (InitScore * scoreMulti * level);
    }


    public override void OnDeath()
    {
        enabled = false;
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Die");
    }

    private void Death()
    {
        Debug.Log("died");
        Destroy(gameObject);
    }

    public override void ApplyDamage(float damage)
    {
        Debug.Log($"Ouch! {damage}");
        Health -= damage;
        OnHealthChanged?.Invoke(Health);
        if (Health <= 0)
        {
            GameEvents.EnemySlain(this);
            OnDeath();

        }
    }

    
}
