using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    public Vector3 respawnPoint;
    public float speedDifForExplosion = 10;
    Quaternion rot;
    Rigidbody rb;
    private void Start()
    {
        rot = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") 
        {
            float velo1 = rb.velocity.magnitude;
            float velo2 = collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            if (velo2 > velo1 + speedDifForExplosion)
            {
                ps.Play();
                StartCoroutine("Respawn");
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        transform.position = respawnPoint;
        transform.rotation = rot;
    }
}
