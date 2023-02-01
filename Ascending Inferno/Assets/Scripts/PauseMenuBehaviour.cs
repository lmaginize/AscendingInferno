using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    //vars
    public static bool isPaused;

    //Gets a reference to the pause menu
    [SerializeField] GameObject pauseMenuUI;

    private void Start()
    {
        Resume(); // resets vars just in case
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            //Checks if game is already paused
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
               
        }
    }
    /// <summary>
    /// Resumes the game 
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    private void Pause()
    {
       
        pauseMenuUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
}
