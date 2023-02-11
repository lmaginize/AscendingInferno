using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GenerealTextPopUp : MonoBehaviour
{
    [SerializeField] TMP_Text textObject;
    [SerializeField] string textInput;
    [SerializeField] int duration;

    private void Start()
    {
       // StartCoroutine(GeneralPopUp(duration));//testing purposes
    }
    private IEnumerator GeneralPopUp(float duration)
    {
        if(textObject.text == "")
        {
            textObject.text = textInput;
        }
        yield return new WaitForSeconds(duration);
        textObject.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GeneralPopUp(duration));
        }
    }
}
