using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void Playgame()
    {
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("quited");
        Application.Quit();
    }
}
