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
        /*if (collision.transform.tag == "Player")
        {
            rb.AddForce(collision.GetContact(0).normal * forceMultiplier, ForceMode.Impulse);
        }
        else { rb.AddForce(collision.GetContact(0).normal, ForceMode.Impulse); }*/
        rb.AddForce(Reflect(rb, collision.GetContact(0).normal),ForceMode.Impulse);
    }

    Vector3 Reflect(Rigidbody rb,Vector3 normal)
    {
        Vector3 direction = Vector3.Reflect(rb.velocity.normalized,normal);
        return direction;
    }
}
