using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseSizePickup : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(5f, 5f, 5f) * 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            PickUpManager.current.currentPickupPlayerOne = AvailablePickups.DecreaseSize;

            UI_Manager.current.SetPlayerPickupIcon(AvailablePickupIcons.decreaseSize, 1);

            FreeLocation();

            Destroy(gameObject);
        }

        if (other.tag == "Player2")
        {
            PickUpManager.current.currentPickupPlayerTwo = AvailablePickups.DecreaseSize;

            UI_Manager.current.SetPlayerPickupIcon(AvailablePickupIcons.decreaseSize, 2);

            FreeLocation();

            Destroy(gameObject);
        }
    }

    private void FreeLocation()
    {
        PickUpManager.current.FreeUpSpawnLocation(transform.parent.gameObject);
    }
}
