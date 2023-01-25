using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementBehaviour : MonoBehaviour
{
    public float moveSpeed;
    public GameObject pivotObj; //The object that this GameObject (the one this script is attached to) will rotate around

    public Rigidbody rb;
    public float gravity; //?
    public float jumpForce;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public bool isJumping;
    public float jumpTime;
    public bool canJump;

    public bool isGrounded;
    private float jumpCountTimer;

    public float dashAmount;
    public float startDashTime;
    public float dashTime;
    public bool isDashing;

    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector3(0, rb.velocity.y, (hInput * moveSpeed) * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            canJump = true;
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

        /*
        dashTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q) && isDashing == false)
        {
            dashTime = startDashTime;
        }

        if (dashTime <= 0)
        {
            isDashing = false;
            moveSpeed = 100;
        }
        else
        {
            isDashing = true;
            transform.RotateAround(pivotObj.transform.position, new Vector3(0, 1, 1), dashAmount * Time.deltaTime);
            moveSpeed = 0;
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            mainCamera.transform.parent = null;
            moveSpeed = 0;
            jumpForce = 0;
        }
    }
}