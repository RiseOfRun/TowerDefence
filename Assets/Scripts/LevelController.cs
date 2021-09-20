using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Field GameField;
    public Spawner[] Spawners;
    public GameObject Finish;
    public GameObject Waypoints;
    public bool DynamicMode = true;

    public bool newStart = true;
    public float WaveMultiplier;
    public int currentWave;

    private WaveController waveController;

    // Start is called before the first frame update
    void Start()
    {
        waveController = GetComponentInChildren<DynamicWave>();
        if (waveController!=null)
        {
            DynamicMode = true;
        }
        
        Spawners = GetComponentsInChildren<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
