using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene("Scenes/GameLevel");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }
}
