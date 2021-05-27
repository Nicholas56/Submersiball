using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePickup : MonoBehaviour
{
    public int ownerPlayerNumber;
    
    void Update()
    {
        transform.Rotate(new Vector3(5f, 5f, 5f) * 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1") 
        {
            if(ownerPlayerNumber != 1)
            {

            }
        }

        if(other.tag == "Player2")
        {
            if(ownerPlayerNumber != 2)
            {

            }
        }
    }
}
