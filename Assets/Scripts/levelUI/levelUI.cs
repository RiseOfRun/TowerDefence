using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelUI : MonoBehaviour
{
    public Button StartWaveButton;
    public Text Money;
    public Text Lives;
    public Text WaveNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       
       // Debug.Log($"progres? {LevelController.Instance.WaveInProgress}");
        Money.text = Player.Instance.Money.ToString();
        Lives.text = Player.Instance.Lives.ToString();
        WaveNumber.text = LevelController.Instance.CurrentWave.ToString();
        StartWaveButton.interactable = !LevelController.Instance.WaveInProgress;
    }
}
