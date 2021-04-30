using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    public Vector3 respawnPoint;
    Quaternion rot;
    private void Start()
    {
        rot = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") { ps.Play();StartCoroutine("Respawn"); }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        transform.position = respawnPoint;
        transform.rotation = rot;
    }
}
