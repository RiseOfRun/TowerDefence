
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
        int competedLevels = PlayerPrefs.GetInt("CompletedLevels");
        for (int i = 0; i < GameManager.LevelsCount; i++)
        {
            var newButton = Instantiate(SelectLevelButtonPrefab,Space.transform);
            newButton.onClick.AddListener(() => {OnButtonPress(i);});
            newButton.GetComponentInChildren<TMP_Text>().text = $"Level:\n {i+1}";
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
        SceneManager.LoadScene("Scenes/GameLevel");
    }

    void Close()
    {
        gameObject.SetActive(false);
    }
}
