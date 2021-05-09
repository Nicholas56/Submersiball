using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerGoal : NetworkBehaviour
{
    [SerializeField] ParticleSystem ps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            other.transform.position = Vector3.zero;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ps.Play();
        }
    }

}
