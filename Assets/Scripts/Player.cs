using UnityEngine;

public class Player : MonoBehaviour
{
    public int Money = 100;
    public int Lives;
    public static Player Instance;
    // Start is called before the first frame update

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
