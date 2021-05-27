using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public static PickUpManager current;

    [SerializeField] List<GameObject> allPickupLocations;
    List<GameObject> freePickupLocations;
    List<GameObject> takenPickupLocations;

    [SerializeField] List<GameObject> pickups;

    [SerializeField] int spawnCoolDown;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        freePickupLocations = allPickupLocations;

        StartCoroutine("SpawnPickupTimer");
    }

    IEnumerator SpawnPickupTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCoolDown);

            if (freePickupLocations.Count != 0)
            {
                if (pickups.Count != 0)
                {
                    SpawnPickUp();
                }
                else
                {
                    Debug.LogError("There are no pickups available on the list");
                }
            }
            else
            {
                Debug.LogError("There are no spawn locations available on the list");
            }
        }
    }

    void SpawnPickUp()
    {
        int randomLocationIndex = Random.Range(1, freePickupLocations.Count);

        int randomPickUpIndex = Random.Range(1, pickups.Count);

        Instantiate(pickups[randomPickUpIndex], freePickupLocations[randomPickUpIndex].transform);

        takenPickupLocations.Add(freePickupLocations[randomLocationIndex]);

        freePickupLocations.Remove(freePickupLocations[randomLocationIndex]);
    }

    public void FreeUpSpawnLocation(GameObject location)
    {
        takenPickupLocations.Remove(location);

        freePickupLocations.Add(location);
    }
}
    