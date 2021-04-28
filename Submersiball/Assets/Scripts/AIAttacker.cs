using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttacker : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float turnSpeed = 1.0f;
    Transform ball;
    [SerializeField] [Range(1, 2)] int team = 0;
    Vector3 offset;
    Vector3 newHeading;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (ball == null) { ball = FindObjectOfType<AmplifiedBallHit>().transform; }
        newHeading = (ball.position - transform.position).normalized;
        if (team == 1) { 
            offset = new Vector3(0, 0, -1);
            GetComponent<SubMarineColor>().ChangeColors(GameManager.current.team1Mat);
        }
        else 
        { 
            offset = new Vector3(0, 0, 1);
            GetComponent<SubMarineColor>().ChangeColors(GameManager.current.team2Mat);
        }


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
        newHeading = (ball.position - transform.position+offset).normalized;
    }
}
