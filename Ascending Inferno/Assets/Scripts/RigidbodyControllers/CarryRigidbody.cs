using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryRigidbody : MonoBehaviour
{
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    public Vector3 lastPosition;
    Transform t;
    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        lastPosition = t.position;
    }

    void LateUpdate()
    {
        if (rigidbodies.Count > 0)
        {
            for (int i = 0; i < rigidbodies.Count; i++)
            {
                Rigidbody rb = rigidbodies[i];
                Vector3 velocity = (t.position - lastPosition);
                rb.transform.Translate(velocity, t);
            }
        }

        lastPosition = t.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Add(rb);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Remove(rb);
        }
    }

    void Add(Rigidbody rb)
    {
        if (!rigidbodies.Contains(rb))
        {
            rigidbodies.Add(rb);
        }
    }
    void Remove(Rigidbody rb)
    {
        if (rigidbodies.Contains(rb))
        {
            rigidbodies.Remove(rb);
        }
    }
}
