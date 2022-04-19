using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : Targetable
{
    public float Speed = 0;
    public int Score = 0;
    public int Penalty = 1;
    public Transform[] Waypoints;
    public float FlyHeight;
    [HideInInspector] public Queue<Vector3> Path = new Queue<Vector3>();
    public float PercentComplete => passedDistance / fullDistance;
    private float fullDistance = 0;
    private float passedDistance = 0;
    private bool startedMove = false;

    void Awake()
    {
    }

    void CalculateDistance()
    {
        Vector3 currentPosition = transform.position;
        foreach (var point in Waypoints)
        {
            Path.Enqueue(point.position);
            fullDistance += Vector3.Distance(currentPosition, point.position);
            currentPosition = point.position;
        }
    }
    void Start()
    {
        CalculateDistance();
    }

    public void ReturnToStart()
    {
        passedDistance = 0;
        Path = new Queue<Vector3>();
        transform.position = Waypoints[0].transform.position;
        fullDistance = 0;
        CalculateDistance();
        gameObject.SetActive(false);
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
        Move();
        CorrectHeight();
    }

    private void Move()
    {
        Vector3 target = Path.Peek();
        Vector3 position = transform.position;
        
        Vector3 nextPos = Vector3.MoveTowards(position, startedMove
            ? new Vector3(target.x, position.y, target.z)
            : new Vector3(target.x, target.y, target.z), Time.deltaTime * Speed);
        
        passedDistance += Vector3.Distance(nextPos, position);
        position = nextPos;
        transform.position = position;
        if (startedMove)
        {
            if (!Mathf.Approximately(transform.position.x, target.x) ||
                !Mathf.Approximately(transform.position.z, target.z)) return;
        }
        else
        {
            if (transform.position != target)
            {
                return;
            }
        }

        Path.Dequeue();
        startedMove = true;
    }

    private void CorrectHeight()
    {
        if (!startedMove)
        {
            return;
        }

        Vector3 position = transform.position;
        Ray ray = new Ray(new Vector3(position.x, 20, position.z), Vector3.down);
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Ground")))
        {
            return;
        }

        Vector3 point = ray.GetPoint(hit.distance);
        if (Math.Abs(point.y - position.y + FlyHeight) > 0.00001)
        {
            transform.position = new Vector3(transform.position.x, point.y + FlyHeight, transform.position.z);
        }
    }

    public override void OnDeath()
    {
        GameEvents.EnemySlain(this);
        enabled = false;
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Die");
        Destroy(this);
    }

    private void Death()
    {
        Debug.Log("died");
        Destroy(gameObject);
    }

    public override void ApplyDamage(float damage)
    {
        if (Health <= 0)
        {
            return;
        }

        // Debug.Log($"Ouch! {damage}");
        Health -= damage;
        OnHealthChanged?.Invoke(Health);
        if (Health <= 0)
        {
            OnDeath();
        }
    }
}