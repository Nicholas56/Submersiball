using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePickup : MonoBehaviour
{
    int pickupID = 1;

    void Update()
    {
        transform.Rotate(new Vector3(5f, 5f, 5f) * 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            PickUpManager.current.currentPickupPlayerOne = AvailablePickups.Mine;

            FreeLocation();

            Destroy(gameObject);
        }

        if(other.tag == "Player2")
        {
            PickUpManager.current.currentPickupPlayerTwo = AvailablePickups.Mine;

            FreeLocation();

            Destroy(gameObject);
        }
    }

    private void FreeLocation()
    {
        PickUpManager.current.FreeUpSpawnLocation(transform.parent.gameObject);
    }
}
