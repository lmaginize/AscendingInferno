using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementBehaviour : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject pivotObj; //The object that this GameObject (the one this script is attached to) will rotate around

    public Rigidbody rb;
    public float gravity; //?
    public float jumpHeight;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public bool isJumping;
    public float jumpTime;
    public bool canJump;

    Vector3 velocity;
    public bool isGrounded;
    private float jumpCountTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            isJumping = true;
            jumpCountTimer = jumpTime;
            rb.velocity = new Vector3(rb.velocity.x, 10, rb.velocity.z);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpCountTimer > 0)
            {
                //rb.velocity = new Vector3(rb.velocity.x, 10, rb.velocity.z);
                jumpCountTimer -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            canJump = false;
        }

        transform.RotateAround(pivotObj.transform.position, new Vector3(0, 1, 0), (hInput * rotationSpeed) * Time.deltaTime);
    }

    /*
     public float gravity;

    public bool isJumping;
    public float jumpTime;
    public bool canJump;
    public bool canShowJumpIcon = true;

    Vector3 velocity;
    public bool isGrounded;

    private float jumpCountTimer;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(isGrounded)
        {
            canJump = true;
            canShowJumpIcon = true;
        }

        float z = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(1) && canJump == true)
        {
            isJumping = true;
            jumpCountTimer = jumpTime;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }



        if (Input.GetMouseButton(1) && isJumping == true)
        {
            if (jumpCountTimer > 0)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpCountTimer -= Time.deltaTime;
            }
            else
            { 
                isJumping = false;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isJumping = false;
            canJump = false;
            canShowJumpIcon = false;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        */
}


/*
     public float gravity;

    public bool isJumping;
    public float jumpTime;
    public bool canJump;
    public bool canShowJumpIcon = true;

    Vector3 velocity;
    public bool isGrounded;

    private float jumpCountTimer;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(isGrounded)
        {
            canJump = true;
            canShowJumpIcon = true;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(1) && canJump == true)
        {
            canShowJumpIcon = false;
            isJumping = true;
            jumpCountTimer = jumpTime;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetMouseButton(1) && isJumping == true)
        {
            if (jumpCountTimer > 0)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpCountTimer -= Time.deltaTime;
            }
            else
            { 
                isJumping = false;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isJumping = false;
            canJump = false;
            canShowJumpIcon = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            shieldIsActive = true;
        }

        shield.SetActive(shieldIsActive);
        shieldSquare.SetActive(shieldIsActive);
        jumpSquare.SetActive(canShowJumpIcon);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (move.magnitude != 0)
        {
            speed += accel * Time.deltaTime;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        }
        else
        {
            speed -= decel * Time.deltaTime;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            negativeZ = -1;
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            negativeZ = 1;
        }

        /*
        if (x >= 1 || z >= 1 || x >= 1 && z >= 1)
        {
            speed += accel * Time.deltaTime;
            speed = Mathf.Clamp(speed, 12, maxSpeed);
        }
        else if (x < 1 || z < 1 || x < 1 && z < 1)
        {
            speed -= decel * Time.deltaTime;
            speed = Mathf.Clamp(speed, 12, maxSpeed);
        }
        */