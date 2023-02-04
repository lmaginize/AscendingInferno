using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameController : MonoBehaviour
{
   
    public GameObject startPopUp,endPopUp;

    public TMP_Text health;
    private playerMovementBehaviour playerScript;
    void Start()
    {
        playerScript = FindObjectOfType<playerMovementBehaviour>();
        playerMovementBehaviour.isDone = false;
    }

   
    void Update()
    {
        Destroy(startPopUp, 3); 

        if(playerMovementBehaviour.isDone)
        {
            endPopUp.SetActive(true);
            Destroy(endPopUp, 3);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateHealthUI()
    {
        health.text = "Health: " + playerScript.health;
    }
   
}
