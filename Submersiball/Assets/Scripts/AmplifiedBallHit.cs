using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplifiedBallHit : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

        }
        else {        }
    }
}
