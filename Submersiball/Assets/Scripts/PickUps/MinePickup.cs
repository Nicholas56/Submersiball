using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePickup : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(5f, 5f, 5f) * 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            PickUpManager.current.currentPickupPlayerOne = AvailablePickups.Mine;

            UI_Manager.current.SetPlayerPickupIcon(AvailablePickupIcons.Mine, 1);

            FreeLocation();

            Destroy(gameObject);
        }

        if(other.tag == "Player2")
        {
            PickUpManager.current.currentPickupPlayerTwo = AvailablePickups.Mine;

            UI_Manager.current.SetPlayerPickupIcon(AvailablePickupIcons.Mine, 2);

            FreeLocation();

            Destroy(gameObject);
        }
    }

    private void FreeLocation()
    {
        PickUpManager.current.FreeUpSpawnLocation(transform.parent.gameObject);
    }
}
