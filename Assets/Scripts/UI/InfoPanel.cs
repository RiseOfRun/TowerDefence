using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public Button StartWaveButton;
    public TMP_Text Money;
    public TMP_Text Lives;
    public TMP_Text WaveNumber;
    [FormerlySerializedAs("Timer")] public TMP_Text StartButtonText;
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
        Money.text = ((int)Player.Instance.Money).ToString();
        Lives.text = Player.Instance.Lives.ToString();
        WaveNumber.text = $"{LevelController.Instance.CurrentWave:00}/{LevelController.Instance.WaveCount:00}";
        StartWaveButton.interactable = !LevelController.Instance.WaveInProgress;
        if (!LevelController.Instance.WaveInProgress)
        {
            StartButtonText.text = $"{(int)LevelController.Instance.TimeToWave}";
        }
        else
        {
            StartButtonText.text = $"";
        }
    }
}
