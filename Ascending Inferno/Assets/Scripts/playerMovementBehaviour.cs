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

    public bool isPlayerFacingRight;
    public Transform playerTransform;
    public Vector3 ledgePosAlteration; //The amount we will move the player upon him hooking onto a ledge
    public float ledgeDistance;
    public bool isHangingOntoLedge;
    public bool canJumpOffLedge;
    public float ledgeCheckSide;
    public Vector3 verticalLedgeCheckBuffer;
    public Vector3 horizontalLedgeCheckBuffer;
    public GameObject horizontalLedgeCheckPrefab;
    public GameObject horizontalLedgeCheckObj; //This gets deleted
    public float horizontalLedgeCheckBufferZ;

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
    public bool canFall;
    
    public GameObject mainCamera;

    public static bool isDone;

    public AudioClip JumpSound;
    private healthKit hk;
    private GameController gc;

    public PhysicMaterial bounceMat;
    public PhysicMaterial normalMat;

    public bool invincible;

    public Transform startingLocation;
    public GameObject Lava;

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
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        dashCoolDownCountDown -= Time.deltaTime;

        verticalLedgeCheckBuffer = new Vector3(0, 1.03f, 0.89f * ledgeCheckSide);
        horizontalLedgeCheckBuffer = new Vector3(0, 0.025f, horizontalLedgeCheckBufferZ);

        ledgePosAlteration = new Vector3(transform.position.x, transform.position.y, ledgeCheckSide * 0.5f);

        if (hInput < 0)
        {
            isPlayerFacingRight = false;
            ledgeCheckSide = -1;
            horizontalLedgeCheckBufferZ = -1.41f;
        } else if(hInput > 0)
        {
            isPlayerFacingRight = true;
            ledgeCheckSide = 1;
            horizontalLedgeCheckBufferZ = 0.51f;
        }

        if (canMove == true && isHangingOntoLedge == false)
        {
            switch(PlayerState)
            {
                case playerMoveState.SideScrollerView:
                    rb.velocity = new Vector3(0, rb.velocity.y, hInput * moveSpeed);
                    rb.constraints = RigidbodyConstraints.FreezePositionX;
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    break;
                case playerMoveState.BehindTheBackView:
                    rb.velocity = new Vector3(hInput * moveSpeed, rb.velocity.y, vInput * moveSpeed);
                    rb.constraints = RigidbodyConstraints.FreezePositionZ;
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    break;
                default: //The default is the sidescroller controls
                    rb.velocity = new Vector3(0, rb.velocity.y, hInput * moveSpeed);
                    rb.constraints = RigidbodyConstraints.FreezePositionX;
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
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

        if (isDashing == false && isHangingOntoLedge == false && invincible == false)
        {
            playerMat.color = Color.white;
        }

        //isHangingOntoLedge = Physics.CheckSphere(ledgeCheck.position, ledgeDistance, groundMask);

        if (isHangingOntoLedge)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;

            timesJumped = 1;
            playerMat.color = Color.blue;

            canFall = false;
            canMove = false;
            canDash = false;
            rb.useGravity = false;

            canJump = true;
            canJumpOffLedge = true;
        } else
        {
            canMove = true;
            canFall = true;
            rb.useGravity = true;
            Destroy(horizontalLedgeCheckObj);
        }

        /*
        if(timesJumped < maxAmountOfJumps)
        {
            canJump = true;
        }
        */

        if (Input.GetKeyDown(KeyCode.Space) && (canJump == true || (isHangingOntoLedge && canJumpOffLedge == true)))
        {
            timesJumped++;
            isJumping = true;
            jumpCountTimer = jumpTime;
            AudioSource.PlayClipAtPoint(JumpSound, playerTransform.position);
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + jumpForce, rb.velocity.z);
            if(isHangingOntoLedge && canJumpOffLedge == true)
            {
                rb.velocity = new Vector3(rb.velocity.x, 1000, rb.velocity.z);
                isHangingOntoLedge = false;
            }
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
            Time.timeScale = 1f;
            Invoke("Respawn", 2);
        }

        //Debug.DrawRay(transform.position + horizontalLedgeCheckBuffer, transform.TransformDirection(Vector3.left) * 1f, Color.red);
    }

    private void FixedUpdate()
    {
        if(canFall == true)
        {
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            rb.AddForce(gravity, ForceMode.Acceleration);
            print(rb.velocity.y);
            if (rb.velocity.y < 0)
            {
                if (Physics.Raycast(transform.position + verticalLedgeCheckBuffer, transform.TransformDirection(Vector3.down), out RaycastHit hitInfo, 1f, groundMask))
                {
                    isHangingOntoLedge = true;
                    //Debug.DrawRay(transform.position + verticalLedgeCheckBuffer, transform.TransformDirection(Vector3.down) * 1, Color.red);
                    print("On Ledge");
                }
            }
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

        if (other.gameObject.CompareTag("CheckPoint"))
        {
            startingLocation.position = other.transform.position;
        }
/*
        if (other.gameObject.CompareTag("CrouchZone"))
        {
            gameObject.transform.localScale = new Vector3(1, 0.5f, 1);
            playerMat.color = Color.red;
            isCrouched = true;
            inCrouchZone = true;
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("SpikeArea"))
        {
            GetComponent<Collider>().material = normalMat;
        }

       /* if (other.gameObject.CompareTag("CrouchZone"))
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            isCrouched = false;
            inCrouchZone = false;
        }*/
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

        if (other.gameObject.CompareTag("Hazard"))
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

    public void Respawn()
    {
        if (health == 0)
        {
            Time.timeScale = 1f;
            transform.position = startingLocation.position;
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            mainCamera.transform.parent = playerTransform;
            mainCamera.transform.position = new Vector3(playerTransform.position.x + 8.38f, playerTransform.position.y + 3.4f, playerTransform.position.z);
            canMove = true;
            jumpForce = 15;
            Lava.transform.position = new Vector3(0, gameObject.transform.position.y - 290f, 0);
            Lava.GetComponent<lavaBehaviour>().lavaSpeed = 213;
            health = 3;
            gc.UpdateHealthUI();
        }
    }
}