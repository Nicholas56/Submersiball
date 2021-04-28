using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    Rigidbody rb;

    [Header("Movement Controls")]
    [SerializeField] KeyCode accelerateButton;
    [SerializeField] KeyCode boostButton;

    [Header("Movement Values")]
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float maxSpeed = 10.0f;
    [SerializeField] float boostSpeed = 2f;
    [SerializeField] float boostTime = 2;
    [SerializeField] float maxBoostTime = 2;

    [Header("Misc")]
    [SerializeField] bool boost = true;
    [SerializeField] bool accel = false;
    [SerializeField] ParticleSystem boostParticles;
    [SerializeField] [Range(1, 2)] int team = 0;

    float h;
    float v;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (team == 1)
        {
            GetComponent<SubMarineColor>().ChangeColors(GameManager.current.team1Mat);
        }
        else
        {
            GetComponent<SubMarineColor>().ChangeColors(GameManager.current.team2Mat);
        }
    }
    private void Update()
    {
        //Key to boost, if boost is available
        if (Input.GetKey(boostButton) && boostTime>0 && boost)
        {
            boostParticles.Play();
            rb.AddForce(transform.forward * boostSpeed,ForceMode.Acceleration);
            boostTime = Mathf.Max(boostTime - Time.deltaTime, 0);
            if (boostTime <= 0.2f) { boost = false; }
        }
        else { boostTime = Mathf.Min(boostTime + Time.deltaTime, maxBoostTime); if (boostTime == maxBoostTime) { boost = true; }  }

        //Key to initiate acceleration
        if (Input.GetKeyDown(accelerateButton)) { accel = !accel; }
        if (accel) 
        {
            rb.AddForce(transform.forward * moveSpeed,ForceMode.Force);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.AddForce(transform.forward * -moveSpeed, ForceMode.Force);
            }
        } //else { rb.velocity = Vector3.zero; }
    }
    void FixedUpdate()
    {
        //Takes the two dimensional inputs to calculate direction to move in
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(v,h));
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + v, transform.localEulerAngles.y + h, transform.localEulerAngles.z);
    }
}
