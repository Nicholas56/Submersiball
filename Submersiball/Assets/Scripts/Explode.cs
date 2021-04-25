using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") { ps.Play(); }
    }
}
