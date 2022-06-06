using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Button NextLevelButton;

    private void Start()
    {
        if (GameManager.CurrentLevel >= GameManager.LevelsCount - 1)
        {
            NextLevelButton.gameObject.SetActive(false);
        }
        else
        {
            NextLevelButton.gameObject.SetActive(true);
        }
    }
}
