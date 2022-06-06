using UnityEngine;

public class Player : MonoBehaviour
{
    public float Money = 100;
    public int Lives;

    public static Player Instance;
    // Start is called before the first frame update

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