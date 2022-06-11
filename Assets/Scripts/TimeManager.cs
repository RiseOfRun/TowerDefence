using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
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


    [SerializeField] private float currentTimeScale = 1;
    private float fixedDeltaTime;
    void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        CurrentTimeScale = currentTimeScale;

        if (Instance==null)
        {
            Instance = this;
        }
        else if (Instance==this)
        {
            Destroy(gameObject);
        }
    }
}
