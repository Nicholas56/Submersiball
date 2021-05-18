using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    [SerializeField] float explosionRange = 10f;
    ParticleSystem ps;
    [SerializeField] [Tooltip("If set to zero, mine will not respawn")] float respawnTime = 0;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach(Collider col in hitColliders)
        {
            if (col.tag == "Player")
            {
                col.GetComponent<Explode>().Explosion();
            }
        }
        ExplodeMine();
    }

    void ExplodeMine()
    {
        ps.Play();
        if (respawnTime > 0) { StartCoroutine("Respawn"); }
        gameObject.SetActive(false);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);

        gameObject.SetActive(true);
    }
}
