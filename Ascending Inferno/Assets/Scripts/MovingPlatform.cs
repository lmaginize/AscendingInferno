using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Tooltip ("Current Speed of the Platform")]
    public float Speed = 5;

    //public float forwardsSpeed = 5;
    //public float reverseSpeed = -5;
    public Rigidbody rb;
    public bool active = false;
    public int pattern;

    public GameObject position1;
    public GameObject position2;
    public GameObject startPos;
    public bool towards2 = false;

    public GameObject player;
    public bool stopping;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        player = GameObject.Find("PlayerObj");
    }

    // Update is called once per frame
    void FixedUpdate()
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
            //rb.MovePosition(transform.position + new Vector3(0,0,1) * Time.deltaTime * Speed/500);
            if (transform.position.z >= position1.transform.position.z)
            {
                if (stopping == false)
                {
                    towards2 = true;
                }
                else if (stopping == true)
                {
                    Speed = 0;
                }
            }
            if (transform.position.z <= position2.transform.position.z)
            {
                if (stopping == false)
                {
                    towards2 = false;
                }
                else if (stopping == true)
                {
                    Speed = 0;
                }
            }

            if (towards2 == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + -1 * (transform.position - position2.transform.position), Speed * Time.deltaTime);
            }
            else if (towards2 == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + -1 * (transform.position - position1.transform.position), Speed * Time.deltaTime);
            }
        }
        

        if (pattern == 1)
        {
            //rb.velocity = new Vector3(0, Speed * Time.deltaTime, 0);
            if (transform.position.y >= position1.transform.position.y)
            {
                if (stopping == false)
                {
                    towards2 = true;
                }
                else if (stopping == true)
                {
                    Speed = 0;
                }
            }
            if (transform.position.y <= position2.transform.position.y)
            {
                if (stopping == false)
                {
                    towards2 = false;
                }
                else if (stopping == true)
                {
                    Speed = 0;
                }
            }
            if (towards2 == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + -1 * (transform.position - position2.transform.position), Speed * Time.deltaTime);
            }
            else if (towards2 == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + -1 * (transform.position - position1.transform.position), Speed * Time.deltaTime);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.Equals(position1))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (transform.position - position2.transform.position), 5 * Time.deltaTime);
        }
        if (other.gameObject.Equals(position2))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (transform.position - position1.transform.position), 5 * Time.deltaTime);
        }*/

        if (other.tag == "Player")
        {
            other.transform.parent.transform.SetParent(this.transform);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.transform.SetParent(null);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.CompareTag("Player")) && (pattern == 1))
        {
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<playerMovementBehaviour>().jumpForce = 16;
        }
    }
    public void OnCollisionExit(Collision other)
    {
        if ((other.gameObject.CompareTag("Player")) && (pattern == 1))
        {
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<playerMovementBehaviour>().jumpForce = 12;
        }
    }
}
