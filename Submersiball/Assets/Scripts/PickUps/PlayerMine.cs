using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMine : MonoBehaviour
{
    [SerializeField] float explosionRange = 10f;
    ParticleSystem ps;
    [SerializeField] [Tooltip("If set to zero, mine will not respawn")] float respawnTime = 0;

    int ownerImmunity = 0;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        if (respawnTime > 0 && respawnTime <= 3) { respawnTime = 3.5f; }
    }

    private void Start()
    {
        StartCoroutine("OwnerImmunity");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ownerImmunity == 0)
        {
            if (other.tag != transform.tag)
            {
                other.GetComponent<Explode>().Explosion();

                transform.GetChild(0).gameObject.SetActive(false);

                StartCoroutine("DestroyGameObject");

                ExplodeMine();

                GameEvents.current.MineExplode();
            }
        }
        else
        {
            if (other.tag == "Player1" || other.tag == "Player2")
            {

                    other.GetComponent<Explode>().Explosion();

                    transform.GetChild(0).gameObject.SetActive(false);

                    StartCoroutine("DestroyGameObject");

                    ExplodeMine();

                    GameEvents.current.MineExplode();              
            }
        }     
    }

    void ExplodeMine()
    {
        ps.Play();
    }

    IEnumerator OwnerImmunity()
    {
        yield return new WaitForSeconds(1);

        ownerImmunity += 1;
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }
}
