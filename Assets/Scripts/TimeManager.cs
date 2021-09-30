using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float CurrentTimeScale
    {
        get => currentTimeScale;
        set
        {
            currentTimeScale = value;
            Time.timeScale = currentTimeScale;
            Time.fixedDeltaTime = currentTimeScale * fixedDeltaTime;
        }
    }


    [SerializeField] private float currentTimeScale;
    private float fixedDeltaTime;
    // Start is called before the first frame update
    void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        CurrentTimeScale = currentTimeScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
