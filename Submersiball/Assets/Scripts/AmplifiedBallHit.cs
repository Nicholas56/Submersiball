using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplifiedBallHit : MonoBehaviour
{
    [SerializeField] float forceMultiplier;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(collision.GetContact(0).normal * forceMultiplier, ForceMode.Impulse);
    }
}
