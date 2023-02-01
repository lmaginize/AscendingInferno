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

    public Transform playerTransform;
    public Transform ledgeCheck;
    public float ledgeDistance;
    public bool isHangingOntoLedge;
    public bool isScalingLedge;

    public bool isGrounded;
    private float jumpCountTimer;

    public float dashXAmount;
    public float dashYAmount;
    public float startDashTime;
    public float dashTime;
    public bool isDashing;
    public float dashY;
    public float dashZ;

    public float gravityScale;
    public static float globalGravity = -9.81f;
    public bool canFall;

    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        playerTransform = GetComponent<Transform>();
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

        isHangingOntoLedge = Physics.CheckSphere(ledgeCheck.position, ledgeDistance, groundMask);

        if(isHangingOntoLedge)
        {
            isScalingLedge = true;
        }

        RaycastHit groundCheckForNothingWhileScaling;

        if (isScalingLedge)
        { 
            //groundDistance = 0.3f;
            canFall = false;
            canMove = false;
            canDash = false;
            canJump = false;
            if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out groundCheckForNothingWhileScaling, Mathf.Infinity, groundMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * groundCheckForNothingWhileScaling.distance);
                Debug.Log("Start moving forward");
                //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 1);
            } else
            {
                rb.velocity = new Vector3(rb.velocity.x, 1, rb.velocity.z);
            }
        } else
        {
            canFall = true;
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

        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(0)) && isDashing == false && canDash == true)
        {
            dashTime = startDashTime;
            dashZ = Input.GetAxisRaw("Horizontal");
            dashY = Input.GetAxisRaw("Vertical");
        }

        if (dashTime <= 0)
        {
            isDashing = false;
            if(isHangingOntoLedge == false)
            {
                canMove = true;
            }
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

    private void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        if(canFall == true)
        {
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            mainCamera.transform.parent = null;
            canMove = false;
            canJump = false;
        }
    }
}