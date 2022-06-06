using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button ResumeButton;

    public void Start()
    {
        if (ResumeButton!=null)
        {
            if (PlayerPrefs.GetInt("OnLevel",-1)!=-1)
            {
                ResumeButton.gameObject.SetActive(true);
            }
            else
            {
                ResumeButton.gameObject.SetActive(false);
            }
        }
        
    }

    public void ResumeGame()
    {
        SceneManager.LoadScene("Scenes/GameLevel");
    }
}
