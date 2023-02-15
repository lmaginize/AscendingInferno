using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpikeBehaviour : MonoBehaviour
{
    public playerMovementBehaviour pmb;
    public PhysicMaterial bounceMat;
    public PhysicMaterial normalMat;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
     
        print("trigger colliding");

        if (other.gameObject.CompareTag("Lava"))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            pmb.Lavaed();
        }

        if (other.gameObject.CompareTag("EndTrigger"))
        {
            pmb.Doned();
        }

        if (other.gameObject.CompareTag("SpikeArea"))
        {
            gameObject.GetComponent<Collider>().material = bounceMat;
            pmb.Bounced();
            print("spikedarea");
        }

        if (other.gameObject.CompareTag("HealthKit"))
        {
            pmb.Healthed();
        }

        if (other.gameObject.CompareTag("CheckPoint"))
        {
            pmb.PointChecked();
            pmb.checkHealthed();
        }
        if (other.gameObject.CompareTag("CrouchZone"))
        {
            pmb.CrouchZoning();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SpikeArea"))
        {
            gameObject.GetComponent<Collider>().material = normalMat;
            pmb.BounceNo();
        }

        if (other.gameObject.CompareTag("CrouchZone"))
        {
            pmb.CrouchZoneNo();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        print("collision colliding");

        if (other.gameObject.CompareTag("Spike"))
        {
            pmb.Spiked();

        }

        if (other.gameObject.CompareTag("Hazard"))
        {
            pmb.Hazared();
        }

    }
}
