using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerExplode : NetworkBehaviour
{
    [SerializeField] ParticleSystem ps;

    Vector3 respawnPoint;
    [SerializeField] float speedDifForExplosion = 10;
    Quaternion rot;
    Rigidbody rb;
    public override void OnStartAuthority()
    {
        rot = transform.rotation;
        respawnPoint = transform.position;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    [ClientCallback]
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            float velo1 = rb.velocity.magnitude;
            float velo2 = collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            Debug.Log("Speed1: " + velo1 + " Speed2: " + velo2);
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
        CmdRespawn();
    }
    [Command]
    void CmdRespawn()
    {
        transform.position = respawnPoint;
        transform.rotation = rot;
    }
}
