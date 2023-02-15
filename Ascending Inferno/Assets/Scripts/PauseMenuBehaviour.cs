using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class PauseMenuBehaviour : MonoBehaviour
{
    //vars
    public static bool isPaused;
    bool canPauseAgain;
    //Gets a reference to the pause menu
    [SerializeField] GameObject pausePanel;
    [SerializeField] TMP_Text mouseValue, audioValue;
    [SerializeField] Slider mouseSlider, audioSlider;
    [SerializeField] GameObject UI;

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
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) )
        {
            if (isPaused)
            {
                //checks if you are in the pause menu to resume again
                //prevents people spamming pause in other menus.
                if (canPauseAgain) 
                {
                    Resume();
                }
            }
            else
            {
                Pause();
            }
        }

        checkPauseOpen();
    }
    /// <summary>
    /// Resumes the game 
    /// </summary>
    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pausePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        UI.SetActive(true);
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    private void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
        UI.SetActive(false);
    }

    public void AudioSlider()
    {
        audioValue.text = audioSlider.value.ToString("F0");
    }

    public void MouseSlider()
    {
        mouseValue.text = mouseSlider.value.ToString("F0");
    }

    //prob a better way to do this.
    void checkPauseOpen()
    {
        if(pausePanel.activeSelf == true)
        {
            canPauseAgain = true;
        }
        else
        {
            canPauseAgain = false;
        }
    }
}

