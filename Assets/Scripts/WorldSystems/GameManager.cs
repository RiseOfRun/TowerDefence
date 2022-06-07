using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int CurrentLevel = -1;
    public static int LevelsCount = 3;
    public LevelController[] Levels;
    public LevelController Level;
    public bool ForceLoad;
    public LevelLoader Loader;

    private bool newGame = true;

    public void Awake()
    {
        Time.fixedDeltaTime = Time.fixedUnscaledDeltaTime;
        Time.timeScale = 1;
        if (ForceLoad)
        {
            return;
        }
        int levelState = PlayerPrefs.GetInt("OnLevel",-1);
        if (levelState == -1)
        {
            StartNewLevel();
        }
        else
        {
            CurrentLevel = levelState;
            LoadLevel();
        }
    }

    public void StartNewLevel()
    {
        PlayerPrefs.SetInt("OnLevel", CurrentLevel);
        Level = Levels[CurrentLevel];
        Level.gameObject.SetActive(true);
        Loader.SetLayout(Level);
        Loader.Invoke("SaveLevel",0.1f); //??
        
    }

    public void LoadLevel()
    {
        Level = Levels[CurrentLevel];
        Level.gameObject.SetActive(true);
        newGame = false;
        Loader.SetLayout(Level);
        Loader.needToLoad = true;

    }

    public void RestartLevel()
    {
        PlayerPrefs.SetInt("OnLevel",-1);
        SceneManager.LoadScene("GameLevel", LoadSceneMode.Single);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Scenes/Menu", LoadSceneMode.Single);
    }

    public void GoToNextLevel()
    {
        if (CurrentLevel<LevelsCount-1)
        {
            CurrentLevel++;
            PlayerPrefs.SetInt("OnLevel",-1);
            SceneManager.LoadScene("Scenes/GameLevel", LoadSceneMode.Single);
        }
    }
}
