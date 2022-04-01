using System;
using UnityEngine;
using UnityEngine.UI;

public class levelUI : MonoBehaviour
{
    public Button StartWaveButton;
    public Text Money;
    public Text Lives;
    public Text WaveNumber;
    public Text Timer;
    public ScoreAlert ScoreTablet;

    private void Start()
    {
        GameEvents.OnEnemySlain.AddListener(OnEnemySlain);
        StartWaveButton.onClick.AddListener(LevelController.Instance.NextWave);
    }

    private void OnEnemySlain(Enemy arg0)
    {
        var tablet = Instantiate(ScoreTablet, transform);
        tablet.Origin = arg0.transform.position;
        tablet.Count = arg0.Score;
    }

    void Update()
    {       
       // Debug.Log($"progres? {LevelController.Instance.WaveInProgress}");
        Money.text = Player.Instance.Money.ToString();
        Lives.text = Player.Instance.Lives.ToString();
        WaveNumber.text =
            $"{LevelController.Instance.CurrentWave:00}/{LevelController.Instance.WaveCount:00}";
        StartWaveButton.interactable = !LevelController.Instance.WaveInProgress;

        if (!LevelController.Instance.WaveInProgress)
        {
            Timer.text = ((int)LevelController.Instance.TimeToWave).ToString();
            Timer.enabled = true;
        }
        else
        {
            Timer.enabled = false;
        }
    }
}
