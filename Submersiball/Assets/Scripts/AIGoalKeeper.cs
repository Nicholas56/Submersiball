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
    public bool aim;
    int currentPoint = 0;
    Vector3 currentPointPosition;
    Vector3 newHeading;
    Transform ball;
    [SerializeField] [Range(1, 2)] int team = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        newHeading = (points[currentPoint].position - transform.position).normalized;
        ball = FindObjectOfType<AmplifiedBallHit>().transform; 
        if (team == 1)
        {
            GetComponent<SubMarineColor>().ChangeColors(GameManager.current.team1Mat);
        }
        else
        {
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
        if (Vector3.Distance(transform.position, points[currentPoint].position) < proximity)
        {
            if (aim) { currentPointPosition = FindClostestPoint().position; } 
            else
            {
                currentPoint++;
                if (currentPoint == points.Count) { currentPoint = 0; }
                currentPointPosition = points[currentPoint].position;
            }
        } 
        newHeading = (currentPointPosition - transform.position).normalized; 
    }

    Transform FindClostestPoint()
    {
        Transform closest = points[0];
        for (int i = 1; i < points.Count; i++)
        {
            if (Vector3.Distance(ball.position, points[i].position) < Vector3.Distance(ball.position, closest.position))
            {
                closest = points[i];
            }
        }
        return closest;
    }
}
