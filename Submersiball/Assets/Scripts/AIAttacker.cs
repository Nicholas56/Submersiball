using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttacker : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float turnSpeed = 1.0f;
    [SerializeField] Transform ball;
    Vector3 newHeading;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (ball == null) { ball = FindObjectOfType<AmplifiedBallHit>().transform; }
        newHeading = (ball.position - transform.position).normalized;
    }
    void FixedUpdate()
    {
        // The step size is equal to speed times frame time.
        float singleStep = turnSpeed * Time.deltaTime;
        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, newHeading, singleStep, 0.0f);
        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
        rb.AddForce(transform.forward * moveSpeed, ForceMode.Force);
        newHeading = (ball.position - transform.position).normalized;
    }
}
