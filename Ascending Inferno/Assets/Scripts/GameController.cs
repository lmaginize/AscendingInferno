using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    //The lava is rising pop up
    public GameObject startPopUp,endPopUp;

    void Start()
    {
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
   
}
