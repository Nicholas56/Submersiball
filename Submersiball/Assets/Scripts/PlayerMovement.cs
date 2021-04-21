using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//This script gives the player control over their own movement

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float burstSpeed = 0.1f;
    [SerializeField] float burstTime = 2;
    [SerializeField] float maxBurstTime = 2;
    bool canBurst = true;

    float h;
    float v;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Takes the two dimensional inputs to calculate direction to move in
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 destination = (transform.forward * v) + (transform.right * h);// + transform.position;

        rb.AddForce(destination * moveSpeed);

        if (Input.GetKey(KeyCode.Space) && canBurst)
        {
            rb.AddForce(transform.forward * burstSpeed, ForceMode.Impulse);
            burstTime -= Time.deltaTime;
            if (burstTime <= 0) { canBurst = false; }
        }
        else { burstTime = Mathf.Min(burstTime += Time.deltaTime, maxBurstTime); if (burstTime >= maxBurstTime) { canBurst = true; } }
    }
}
