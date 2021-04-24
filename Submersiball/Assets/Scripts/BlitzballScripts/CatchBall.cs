using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBall : MonoBehaviour
{
    public Transform ballPos;    
    public Transform ball;


    private void Update()
    {
        if (Vector3.Distance(transform.position,ball.position)<3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ball.position = ballPos.position;
                ball.SetParent(transform.parent);
                ball.GetComponent<Rigidbody>().velocity=Vector3.zero;
            }
            if (Input.GetMouseButtonUp(0))
            {
                ball.SetParent(null);
                ball.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
            }
        }
        else { ball.SetParent(null); }
    }
}
