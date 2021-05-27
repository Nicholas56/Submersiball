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
        if (respawnTime > 0 && respawnTime <= 3) { respawnTime = 3.5f; }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach(Collider col in hitColliders)
        {
            if (col.tag != transform.tag)
            {
                col.GetComponent<Explode>().Explosion();
            }
        }
        ExplodeMine();
        GameEvents.current.MineExplode();
    }

    void ExplodeMine()
    {
        ps.Play();
        if (respawnTime > 0) { StartCoroutine("Respawn"); }
        Invoke("RemoveMine", 3);
    }
    void RemoveMine() { gameObject.SetActive(false); }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);

        gameObject.SetActive(true);
    }
}
