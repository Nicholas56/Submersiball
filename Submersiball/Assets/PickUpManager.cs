using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public static PickUpManager current;

    [SerializeField] GameObject[] boostPickupLocations;

    [SerializeField]

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        boostPickupLocations = GameObject.FindGameObjectsWithTag("BoostLocation");
    }

    public void SpawnBoostPickup(Transform spawnLocation, GameObject boostPickup)
    {
        Instantiate(boostPickup, spawnLocation);
    }
}
    