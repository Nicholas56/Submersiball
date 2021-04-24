using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    Rigidbody rb;

    [Header("Movement Values")]
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float boostSpeed = 2f;
    [SerializeField] float boostTime = 2;
    [SerializeField] float maxBoostTime = 2;

    [SerializeField] float maxVerticalRot = 75;
    [SerializeField] float minVerticalRot = -75;
    [SerializeField] bool boost = true;
    [SerializeField] bool accel = false;

    float h;
    float v;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //Mouse click to boost, if boost is available
        if (Input.GetMouseButton(0) && boostTime>0 && boost)
        {
            rb.AddForce(transform.forward * boostSpeed,ForceMode.VelocityChange);
            boostTime = Mathf.Max(boostTime - Time.deltaTime, 0);
            if (boostTime == 0) { boost = false; }
        }
        else { boostTime = Mathf.Min(boostTime + Time.deltaTime, maxBoostTime); if (boostTime == maxBoostTime) boost = true; }

        //Spacebar to initiate acceleration
        if (Input.GetKeyDown(KeyCode.Space)){accel = !accel;}
        if (accel){rb.velocity = transform.forward * moveSpeed; } else { rb.velocity = Vector3.zero; }

        //Takes the two dimensional inputs to calculate direction to move in
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(v,h));
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + v, transform.localEulerAngles.y + h, transform.localEulerAngles.z);
        Debug.Log(transform.localEulerAngles.x);
    }
}
