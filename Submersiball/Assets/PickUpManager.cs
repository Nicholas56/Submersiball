using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public static PickUpManager current;

    GameObject[] allPickupLocations;
    GameObject[] freePickupLocations;
    GameObject[] takenPickupLocations;

    [SerializeField] GameObject[] pickups;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        allPickupLocations = GameObject.FindGameObjectsWithTag("PickupLocation");

        freePickupLocations = allPickupLocations;
    }
}
    