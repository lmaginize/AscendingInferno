using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform shoulderLookAt;

    public GameObject thirdPersonCam;
    public GameObject shoulderCam;
    public GameObject sideScrollCam;

    public Vector3 offset = new Vector3(6f, 2f, 0);

    private bool switchToThirdPersonCam = false;
    private bool switchToSideScrollCam = false;

    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        Basic,
        Shoulder
    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        switchToSideScrollCam = true;
    }

    void Update()
    {
        // switch cameras
        if(Input.GetKeyDown(KeyCode.C))
        {
            switchToSideScrollCam = !switchToSideScrollCam;
            switchToThirdPersonCam = !switchToThirdPersonCam;
        }

        if (switchToSideScrollCam)
        {
            sideScrollCam.SetActive(true);
            thirdPersonCam.SetActive(false);
            //shoulderCam.SetActive(false);

            sideScrollCam.transform.position = playerObj.position + offset;
        }
        else if (switchToThirdPersonCam)
        {
            sideScrollCam.SetActive(false);
            thirdPersonCam.SetActive(true);

            // switch thirdperson styles
            //if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
            //if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Shoulder);

            // rotate orientation
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;

            // rotate player object
            if (currentStyle == CameraStyle.Basic)
            {
                float hInput = Input.GetAxis("Horizontal");
                float vInput = Input.GetAxis("Vertical");
                Vector3 inputDir = orientation.forward * vInput + orientation.right * hInput;

                if (inputDir != Vector3.zero)
                {
                    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
                }
            }

            else if (currentStyle == CameraStyle.Shoulder)
            {
                Vector3 dirToShoulderLookAt = shoulderLookAt.position - new Vector3(transform.position.x, shoulderLookAt.position.y, transform.position.z);
                orientation.forward = dirToShoulderLookAt.normalized;

                playerObj.forward = dirToShoulderLookAt.normalized;
            }
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        shoulderCam.SetActive(false);
        thirdPersonCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Shoulder) shoulderCam.SetActive(true);

        currentStyle = newStyle;
    }
}
