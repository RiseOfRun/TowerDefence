using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Money = 100;
    public int Lives;
    public static Player Instance;
    // Start is called before the first frame update

    void OnEnemyEndPath(Enemy unit)
    {
        Lives -= unit.Penalty;
    }
    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        } else if(Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        GameEvents.OnEnemyEndPath.AddListener(OnEnemyEndPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
