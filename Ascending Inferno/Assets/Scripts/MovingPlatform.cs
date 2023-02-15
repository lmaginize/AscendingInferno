using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Tooltip ("Current Speed of the Platform")]
    public float Speed = 10;

    public float forwardsSpeed = 500;
    public float reverseSpeed = -500;
    public Rigidbody rb;
    public bool active = false;
    public int pattern;

    public GameObject position1;
    public GameObject position2;
    public GameObject startPos;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        player = GameObject.Find("PlayerObj");
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            Movement();
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = startPos.transform.position;
        }
        
        if (player.GetComponent<playerMovementBehaviour>().health == 0)
        {
            active = false;
        }
    }

    public void Movement()
    {
        if (pattern == 0)
        {
            rb.velocity = new Vector3(0, 0, Speed * Time.deltaTime);
        }
        if (pattern == 1)
        {
            rb.velocity = new Vector3(0, Speed * Time.deltaTime, 0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(position1))
        {
            Speed = reverseSpeed;
            
        }
        if (other.gameObject.Equals(position2))
        {
            Speed = forwardsSpeed;
            
        }
    }
}
