using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] Transform ballRestartPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            other.transform.position = ballRestartPos.position;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ps.Play();
        }
    }
}
