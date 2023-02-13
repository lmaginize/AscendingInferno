using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melting : MonoBehaviour
{
    public Animator iceBlockMelt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Heat"))
        {
            GetComponent<Animator>().SetBool("isMelting", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isMelting", false);
        }
    }
}
