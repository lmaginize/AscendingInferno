using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuBehaviour : MonoBehaviour
{
    //Load version 1
    public void PlayVersion1()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayVersion2()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

}
