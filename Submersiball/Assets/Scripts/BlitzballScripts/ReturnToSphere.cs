using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToSphere : MonoBehaviour
{
    public float maxDistance = 26;
    bool outOfSphere = false;
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > maxDistance) { outOfSphere = true; } else { outOfSphere = false; }
        if (outOfSphere)
        {
            rb.AddForce(Vector3.zero - transform.position);
        }
    }
}
