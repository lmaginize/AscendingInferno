using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBoxBehaviour : MonoBehaviour
{
    public float turnMin;
    public float turnMax;

    public float velocityCheck;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Object";
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > velocityCheck)
        {
            gameObject.tag = "Hazard";
            transform.Rotate(Random.Range(turnMin, turnMax) * Time.deltaTime, Random.Range(turnMin, turnMax)
                * Time.deltaTime, Random.Range(turnMin, turnMax) * Time.deltaTime);

        }
        else
        {
            gameObject.tag = "Object";
        }

        if(GetComponent<Rigidbody>().velocity.magnitude == 0)
        {
            Invoke("DeleteThis", 3f);
        }
    }

    public void DeleteThis()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude == 0)
        {
            Destroy(gameObject);
        }
    }
}
