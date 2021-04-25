using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoalKeeper : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float turnSpeed = 1.0f;
    [SerializeField] float proximity = 20.0f;
    [SerializeField] List<Transform> points;
    int currentPoint = 0;
    public enum Behaviour { None, Turning, Moving, Reached}
    public Behaviour state;
    Vector3 newHeading;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        newHeading = (points[currentPoint].position - transform.position).normalized;
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
        if (Vector3.Distance(transform.position, points[currentPoint].position) < proximity)
        {
            currentPoint++;
            if (currentPoint == points.Count) { currentPoint = 0; }
        }
        newHeading = (points[currentPoint].position - transform.position).normalized;
    }
}
