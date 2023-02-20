using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaEnabler : MonoBehaviour
{
    public GameObject lava;
    lavaBehaviour lavaBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        lavaBehaviour = FindObjectOfType<lavaBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lava.SetActive(true);
            lavaBehaviour.lavaEnable();
        }
    }
}
