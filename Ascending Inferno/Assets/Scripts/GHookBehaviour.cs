using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GHookBehaviour : MonoBehaviour
{
    public GameObject hook;
    public GameObject player;
    public float grappleSpeed = 3.0f;
    public LayerMask grappleLayer;

    private bool isGrappling = false;
    private Vector3 grapplePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, Mathf.Infinity, grappleLayer))
            {
                isGrappling = true;
                grapplePoint = hit.point;
                hook.transform.position = hit.point;
                hook.SetActive(true);
            }
        }
        if (isGrappling)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, grapplePoint, grappleSpeed * Time.deltaTime);
        }
        /*
        if (Input.GetMouseButtonUp(0))
        {
            isGrappling = false;
            hook.SetActive(false);
        }
        */
    }
}
