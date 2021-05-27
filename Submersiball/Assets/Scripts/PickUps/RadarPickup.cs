using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPickup : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0f, 5f, 0f) * 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1")
        {

            PickUpManager.current.currentPickupPlayerOne = AvailablePickups.DisableRadar;

            UI_Manager.current.SetPlayerPickupIcon(AvailablePickupIcons.Radar, 1);

            Destroy(gameObject);
        }

        if (other.tag == "Player2")
        {        
            PickUpManager.current.currentPickupPlayerTwo = AvailablePickups.DisableRadar;

            UI_Manager.current.SetPlayerPickupIcon(AvailablePickupIcons.Radar, 2);

            Destroy(gameObject);
        }
    }
}
