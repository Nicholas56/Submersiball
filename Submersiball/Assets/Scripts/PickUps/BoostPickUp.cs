using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPickUp : MonoBehaviour
{
    MeshRenderer meshRen;

    SphereCollider SphereCol;

    Animator anim;

    private void Start()
    {
        meshRen = gameObject.GetComponent<MeshRenderer>();

        SphereCol = gameObject.GetComponent<SphereCollider>();

        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"|| other.tag == "Player1"|| other.tag == "Player2")
        {
            other.GetComponent<SubmarineControl>().StartBoostRefill();

            meshRen.enabled = false;

            SphereCol.enabled = false;

            StartCoroutine("RespawnBoostPickUp", 10);
        }
    }

    private IEnumerator RespawnBoostPickUp(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        anim.SetBool("Grow", true);

        meshRen.enabled = true;

        SphereCol.enabled = true;
    }

    public void DisableGrow()
    {
        anim.SetBool("Grow", false);
    }
}
