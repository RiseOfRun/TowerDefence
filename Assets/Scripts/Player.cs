using UnityEngine;

public class Player : MonoBehaviour
{
    public float Money;
    public int Lives;

    public static Player Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameEvents.OnEnemySlain.AddListener(OnEnemySlain);
    }

    void OnEnemySlain(Enemy unit)
    {
        Money += unit.Score;
    }
}