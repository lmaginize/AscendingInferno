using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCubeSpawnerBehaviour : MonoBehaviour
{
    public bool startSpawning;
    public GameObject fallCube;

    public float spawnTime;
    private float spawnTimeInitial;
    // Start is called before the first frame update
    void Start()
    {
        startSpawning = false;
        spawnTimeInitial = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= 1 * Time.deltaTime;

        if (startSpawning == true && spawnTime < 0)
        {    
            Instantiate(fallCube, transform.position, transform.rotation);
            spawnTime = spawnTimeInitial;

        }
    }

    /* public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startSpawning = true;
        }
    } */

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startSpawning = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startSpawning = false;
        }
    }
}
