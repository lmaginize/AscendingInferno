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
  
    //references to scripts
    private playerMovementBehaviour playerScript;
    private GHookBehaviour ghookScript;

    //vars for UI
    public TMP_Text healthText;
    public Slider healthSlider, ghookSlider;
    public Image healthSprite, ghookSprite;

    int maxSliderHealth;
    void Start()
    {
        playerScript = FindObjectOfType<playerMovementBehaviour>();
        playerMovementBehaviour.isDone = false;
        ghookScript = FindObjectOfType<GHookBehaviour>();

        maxSliderHealth = playerScript.health;
        healthSlider.maxValue = maxSliderHealth;
        healthSlider.value = maxSliderHealth;

        ghookSlider.maxValue = ghookScript.cooldownTime;
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
        ghookSlider.value = ghookScript.cooldown;
    }
}
