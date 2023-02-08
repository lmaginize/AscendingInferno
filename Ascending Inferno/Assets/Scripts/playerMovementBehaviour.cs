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
    public float timesJumped;
    public float maxAmountOfJumps;
    public Material playerMat;

    public enum playerMoveState { SideScrollerView, BehindTheBackView };
    public playerMoveState PlayerState;

    public Animator ledgeCheckAnim;
    public bool isPlayerFacingRight;
    public Transform playerTransform;
    public Transform ledgeCheck;
    public float ledgeDistance;
    public bool isHangingOntoLedge;
    public bool canJumpOffLedge;

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
    private healthKit hk;
    private GameController gc;

    public PhysicMaterial bounceMat;
    public PhysicMaterial normalMat;

    public bool invincible;

    public bool canCrouch;
    public bool isCrouched = false;
    public bool inCrouchZone;


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
        float vInput = Input.GetAxisRaw("Vertical");

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

        if (canMove == true && isHangingOntoLedge == false)
        {
            switch(PlayerState)
            {
                case playerMoveState.SideScrollerView:
                    rb.velocity = new Vector3(0, rb.velocity.y, hInput * moveSpeed);
                    break;
                case playerMoveState.BehindTheBackView:
                    rb.velocity = new Vector3(hInput * moveSpeed, rb.velocity.y, vInput * moveSpeed);
                    break;
                default: //The default is the sidescroller controls
                    rb.velocity = new Vector3(0, rb.velocity.y, hInput * moveSpeed);
                    break;
            }
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            timesJumped = 0;
            canJump = true;
            if(dashCoolDownCountDown <= 0)
            {
                canDash = true;
            }
        } else
        {
            if (!(timesJumped < maxAmountOfJumps))
            {
                canJump = false;
            }
        }

        if (isGrounded == true && isCrouched == false)
        {
            canCrouch = true;
        }
        else
        {
            canCrouch = false;
        }


        if(isDashing == false && isHangingOntoLedge == false && invincible == false && isCrouched == false)
        {
            playerMat.color = Color.white;
        }

        /*
        isHangingOntoLedge = Physics.CheckSphere(ledgeCheck.position, ledgeDistance, groundMask);

        if (isHangingOntoLedge)
        {
            timesJumped = 1;
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            playerMat.color = Color.blue;

            canFall = false;
            canMove = false;
            canDash = false;
            canJump = true;
            //canJumpOffLedge = true;
        } else
        {
            canMove = true;
            canFall = true;
        }
        */

        /*
        if(timesJumped < maxAmountOfJumps)
        {
            canJump = true;
        }
        */

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            timesJumped++;
            //rb.constraints = RigidbodyConstraints.FreezePositionX;
            //rb.constraints = RigidbodyConstraints.FreezePositionZ;
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            //jumpCountTimer = jumpTime;
            //sJumping = true;
            AudioSource.PlayClipAtPoint(JumpSound, playerTransform.position);
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + jumpForce, rb.velocity.z);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpCountTimer > 0)
            {
                jumpCountTimer -= Time.deltaTime;
                //isJumping = true;
            } else
            {
                isJumping = false;
            }
        }

        /*
        if (Input.GetKeyUp(KeyCode.Space) && !(timesJumped < maxAmountOfJumps))
        {

        }
        */

        if (Input.GetKeyDown(KeyCode.LeftControl) && canCrouch == true && canMove == true)
        {
            gameObject.transform.localScale = new Vector3(1, 0.5f, 1);
            playerMat.color = Color.red;
            isCrouched = true;


        }


        if (Input.GetKeyUp(KeyCode.LeftControl) && inCrouchZone == false)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            isCrouched = false;

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
        
        if (other.gameObject.CompareTag("HealthKit"))
        {
            hk = FindObjectOfType<healthKit>();
            if(health < 3){
                health++;
                gc.UpdateHealthUI();
                hk.OnPickup();
            }

        }

        if(other.gameObject.CompareTag("CrouchZone"))
        {
            gameObject.transform.localScale = new Vector3(1, 0.5f, 1);
            playerMat.color = Color.red;
            isCrouched = true;
            inCrouchZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("SpikeArea"))
        {
            GetComponent<Collider>().material = normalMat;
        }

        if (other.gameObject.CompareTag("CrouchZone"))
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            isCrouched = false;
            inCrouchZone = false;
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