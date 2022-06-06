
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelPanel : MonoBehaviour
{
    public Button SelectLevelButtonPrefab;
    public GameObject Space;

    public void Start()
    {
        int competedLevels = PlayerPrefs.GetInt("CompletedLevels",0);
        for (int i = 0; i < GameManager.LevelsCount; i++)
        {
            Button newButton = Instantiate(SelectLevelButtonPrefab,Space.transform);
            int levelNumber = i;
            newButton.onClick.AddListener(() => {OnButtonPress(levelNumber);});
            newButton.GetComponentInChildren<TMP_Text>().text = $"Level:\n{i+1}";
            if (i>competedLevels)
            {
                newButton.interactable = false;
            }
        }
    }

    // Start is called before the first frame update
    void OnButtonPress(int levelNumber)
    {
        GameManager.CurrentLevel = levelNumber;
        PlayerPrefs.SetInt("OnLevel",-1);
        SceneManager.LoadScene("Scenes/GameLevel");
    }

    void Close()
    {
        gameObject.SetActive(false);
    }
}
