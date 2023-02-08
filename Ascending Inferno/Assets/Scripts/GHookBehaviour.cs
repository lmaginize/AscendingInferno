using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GHookBehaviour : MonoBehaviour
{
    public GameObject hook;
    public Rigidbody rb;
    public LineRenderer rope;
    public float grappleSpeed = 3.0f;
    public LayerMask grappleLayer;
    public float cooldownTime = 2.0f;
    public float cooldown = 0f;


    private bool isGrappling = false;
    private Vector3 grapplePoint;
    public float range;

    public AudioSource grapple;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && cooldown <= 0f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.up, out hit, range, grappleLayer))
            {
                isGrappling = true;
                grapplePoint = hit.point;
                hook.transform.position = hit.point;
                hook.SetActive(true);
                rope.enabled = true;
                rope.SetPosition(0, transform.position);
                rope.SetPosition(1, grapplePoint);
                cooldown = cooldownTime;
                grapple.Play();

            }
        }
        if (isGrappling)
        {
            Vector3 direction = (grapplePoint - transform.position).normalized;
            rb.AddForce(direction * grappleSpeed, ForceMode.Acceleration);
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, grapplePoint);
        }
        
        if (Input.GetMouseButtonUp(1) || (grapplePoint - transform.position).magnitude <= 0.1f)
        {
            isGrappling = false;
            hook.SetActive(false);
            rope.enabled = false;
            
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
}
