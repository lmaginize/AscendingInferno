using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementBehaviour : MonoBehaviour
{
    public float moveSpeed;
    //public GameObject pivotObj; //The object that this GameObject (the one this script is attached to) will rotate around

    public Rigidbody rb;
    public float jumpForce;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public bool isJumping;
    public float jumpTime;
    public bool canMove;
    public bool canJump;
    public bool canDash = true;

    public bool isGrounded;
    private float jumpCountTimer;

    public float dashXAmount;
    public float dashYAmount;
    public float startDashTime;
    public float dashTime;
    public bool isDashing;
    public float dashY;
    public float dashZ;

    public GameObject mainCamera;

    public PhysicMaterial bounce;
    public bool spiked;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;
        spiked = false;
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        //float vInput = Input.GetAxisRaw("Vertical");

        if(canMove == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, hInput * moveSpeed);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            canJump = true;
            canDash = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true && isGrounded)
        {
            isJumping = true;
            jumpCountTimer = jumpTime;
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
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

        dashTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q) && isDashing == false && canDash == true)
        {
            dashTime = startDashTime;
            dashZ = Input.GetAxisRaw("Horizontal");
            dashY = Input.GetAxisRaw("Vertical");
        }

         if (dashTime <= 0 && spiked == false)
        {
            isDashing = false;
            canMove = true;
        }
        else
        {
            canDash = false;
            isDashing = true;
            canMove = false;
            isJumping = false;
            if(dashY == 0 && dashZ == 0) //Sets the default dash to a forward horizontal dash if the player has no directional input
            {
                dashY = 0;
                dashZ = 1;
            }
            rb.velocity = new Vector3(rb.velocity.x, dashY * dashYAmount, dashZ * dashXAmount);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            mainCamera.transform.parent = null;
            canMove = false;
            jumpForce = 0;
        }

        if (other.gameObject.CompareTag("Spike"))
        {
            canMove = false;
            spiked = true;
            GetComponent<Collider>().material = bounce;

            Invoke("UnSpike", 1f);

            print("Yoink");
        }

    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            canMove = false;
            GetComponent<Collider>().material = bounce;

            Invoke("UnSpike", 1f);

            print("Yoink");
        }

    } */
    public void UnSpike()
    {
        canMove = true;
        spiked = false;
        GetComponent<Collider>().material = null;

        print("Fine Now");
    }
}