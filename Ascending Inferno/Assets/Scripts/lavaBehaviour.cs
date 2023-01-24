using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaBehaviour : MonoBehaviour
{
    public float lavaSpeed;
    public Rigidbody lavaRB;

    // Start is called before the first frame update
    void Start()
    {
        lavaRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        lavaRB.velocity = new Vector3(0,lavaSpeed * Time.deltaTime,0);
    }
}
