using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
    GameObject ball;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void Update()
    {
        LookAtBall();
    }

    private void LookAtBall()
    {
        transform.LookAt(ball.transform.position);
    }
}
