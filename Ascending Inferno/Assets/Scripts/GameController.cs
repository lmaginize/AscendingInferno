using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{  
   // public GameObject startPopUp,endPopUp; // no longer needed
    [SerializeField] GameObject endTrigger; //final trigger to dictate end state.

    [SerializeField] Transform lavaPos;

    //reference to Scripts
    private playerMovementBehaviour playerScript;
    private GHookBehaviour ghookScript;

    //vars for UI
    public TMP_Text healthText, lavaPosText;
    public Slider healthSlider, grapplingSlider, dashSlider;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //gets a reference to scripts
        playerScript = FindObjectOfType<playerMovementBehaviour>();
        ghookScript = FindObjectOfType<GHookBehaviour>();

        //sets game state
        playerMovementBehaviour.isDone = false;

        //setting values for sliders
        healthSlider.maxValue = playerScript.health;
        healthSlider.value = playerScript.health;

        grapplingSlider.maxValue = ghookScript.cooldownTime;
        grapplingSlider.value = ghookScript.cooldownTime;

        dashSlider.maxValue = playerScript.dashCoolDownTime;
        dashSlider.value = playerScript.dashCoolDownTime;
    }
   
    void Update()
    {
        if(playerMovementBehaviour.isDone)
        {
            EndGameState();
        }
           
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        UpdateGrapplingUI();
        UpdateDashUI();

        string lavaPosNum = (lavaPos.transform.position.y.ToString("F0"));
        lavaPosText.text = "Lava is " + lavaPosNum + " meters from you!";
    }

    /// <summary>
    /// Updates the UI elements for health
    /// is called by playeBehaviour
    /// </summary>
    public void UpdateHealthUI()
    {
        healthText.text = playerScript.health.ToString();
        healthSlider.value = playerScript.health;
    }


    public void UpdateDashUI()
    {
        dashSlider.value = playerScript.dashCoolDownTime;
        if (playerScript.dashCoolDownCountDown >= 0)
        {
            dashSlider.value = playerScript.dashCoolDownCountDown;
        }
    }


    /// <summary>
    /// Updates the UI elements for grappling
    /// is called by playeBehaviour
    /// </summary>
    private void UpdateGrapplingUI()
    {

        grapplingSlider.value = ghookScript.cooldownTime;

        if (ghookScript.cooldown > 0)
        {
            grapplingSlider.value = ghookScript.cooldown;
        }
    }

    /// <summary>
    /// once the player is done with the level the following events will happen
    /// </summary>
    private void EndGameState()
    {
        SceneManager.LoadScene("WinScene");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
