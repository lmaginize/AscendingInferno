using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{  
    public GameObject startPopUp,endPopUp;
    [SerializeField] GameObject endTrigger;

    
    //reference to Scripts
    private playerMovementBehaviour playerScript;
    private GHookBehaviour ghookScript;

    //vars for UI
    public TMP_Text healthText;
    public Slider healthSlider, grapplingSlider;

    void Start()
    {
        playerScript = FindObjectOfType<playerMovementBehaviour>();
        ghookScript = FindObjectOfType<GHookBehaviour>();
        playerMovementBehaviour.isDone = false;

        //setting values for sliders
        healthSlider.maxValue = playerScript.health;
        healthSlider.value = playerScript.health;

        grapplingSlider.maxValue = ghookScript.cooldownTime;
    }
   
    void Update()
    {
        Destroy(startPopUp, 3); 

        if(playerMovementBehaviour.isDone)
        {
            if(endPopUp!= null)
            {
                endPopUp.SetActive(true);
                Destroy(endPopUp, 3);
                endTrigger.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        UpdateGrapplingUI();
    }

    public void UpdateHealthUI()
    {
        healthText.text = "Health: " + playerScript.health;
        healthSlider.value = playerScript.health;
    }

    public void UpdateGrapplingUI()
    {
        grapplingSlider.value = ghookScript.cooldown;
    }
   
}
