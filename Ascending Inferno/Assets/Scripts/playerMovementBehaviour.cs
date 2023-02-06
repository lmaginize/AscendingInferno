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
    public Material playerMat;

    public Animator ledgeCheckAnim;
    public bool isPlayerFacingRight;
    public Transform playerTransform;
    public Transform ledgeCheck;
    public float ledgeDistance;
    public bool isHangingOntoLedge;
    public bool canJumpOffLedge;
    public float ledgeClimbSpeed;

    public bool isGrounded;
    private float jumpCountTimer;

    public float dashXAmount;
    public float dashYAmount;
    public float startDashTime;
    public float dashTime;
    public bool isDashing;
    public float dashY;
    public float dashZ;
    public float dashCoolDownTime;
    public float dashCoolDownCountDown;
    public int health = 3;


    public float gravityScale;
    public static float globalGravity = -9.81f;
    public bool canFall = true;
    
    public GameObject mainCamera;

    public static bool isDone;

    public AudioClip JumpSound;
    private GameController gc;

    public PhysicMaterial bounceMat;
    public PhysicMaterial normalMat;

    public bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        isPlayerFacingRight = true;

        playerMat.color = Color.white;

        playerTransform = GetComponent<Transform>();
        ledgeCheckAnim = ledgeCheck.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        //float vInput = Input.GetAxisRaw("Vertical");

        dashCoolDownCountDown -= Time.deltaTime;

        if (hInput < 0)
        {
            isPlayerFacingRight = false;
            ledgeCheckAnim.Play("ledgeCheckLeft");
        } else if(hInput > 0)
        {
            isPlayerFacingRight = true;
            ledgeCheckAnim.Play("ledgeCheckRight");
        }

        if (canMove == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, hInput * moveSpeed);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            canJump = true;
            if(dashCoolDownCountDown <= 0)
            {
                canDash = true;
            }
        }

        if(isDashing == false && isHangingOntoLedge == false && invincible == false)
        {
            playerMat.color = Color.white;
        }

        isHangingOntoLedge = Physics.CheckSphere(ledgeCheck.position, ledgeDistance, groundMask);

        if (isHangingOntoLedge)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            playerMat.color = Color.blue;

            canFall = false;
            canMove = false;
            canDash = false;
            canJump = true;
            canJumpOffLedge = true;
        } else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            canMove = true;
            canFall = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            isJumping = true;
            jumpCountTimer = jumpTime;
            AudioSource.PlayClipAtPoint(JumpSound, playerTransform.position);
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpCountTimer > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
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
        if (Input.GetKey(KeyCode.W) && isDashing == true)
        {
            isDashing = false;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isDashing = false;
        }
        */

        /*
        if (Input.GetKeyDown(KeyCode.W) && isDashing == false && canDash == true) //&& (isGrounded || canJumpOffLedge))
        {
            if(dashZ == 0)
            {
                AudioSource.PlayClipAtPoint(JumpSound, playerTransform.position);
                dashTime = startDashTime;
                dashY = 1;
            }
            //canFall = true;
            //isDashing = true;
            //canJumpOffLedge = false;
            //jumpCountTimer = jumpTime;
            //rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
        */

        /*
        if (Input.GetKey(KeyCode.W) && isDashing == true)
        {
            isDashing = false;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isDashing = false;
        }
        */

        dashTime -= Time.deltaTime;

        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(0)) && isDashing == false && canDash == true)
        {
            dashY = Input.GetAxisRaw("Vertical");
            dashZ = Input.GetAxisRaw("Horizontal");
            dashTime = startDashTime;
        }

        if (dashTime <= 0)
        {
            isDashing = false;
            if (isHangingOntoLedge == false)
            {
                canMove = true;
            }
        }
        else
        {
            if (!(dashY == 0 && dashZ == 0) && !(dashY >= 1 && dashZ == 0)) //Checks for no input at all, as well as just pressing up
            {
                dashCoolDownCountDown = dashCoolDownTime;
                canDash = false;
                isDashing = true;
                canMove = false;
                isJumping = false;
                playerMat.color = Color.green;
                rb.velocity = new Vector3(rb.velocity.x, dashY * dashYAmount, dashZ * dashXAmount);
            }
            else
            {
                dashTime = 0;
            }

        }

        if (health <= 0)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            mainCamera.transform.parent = null;
            canMove = false;
            jumpForce = 0;
            Time.timeScale = 0f;
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
            health = 0;
        }

        if (other.gameObject.CompareTag("EndTrigger"))
        {
            isDone = true;
        }

        if(other.gameObject.CompareTag("SpikeArea"))
        {
            GetComponent<Collider>().material = bounceMat;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("SpikeArea"))
        {
            GetComponent<Collider>().material = normalMat;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            if (invincible == false)
            {
                health--;
                invincible = true;
                playerMat.color = Color.yellow;
                gc.UpdateHealthUI();

                Invoke("Uninvincible", 1f);
            }
           
        }
    }

    public void Uninvincible()
    {
        invincible = false;
        playerMat.color = Color.white;
    }
}