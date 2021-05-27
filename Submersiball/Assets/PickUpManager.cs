using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AvailablePickups { Empty, Mine, DisableRadar }

public class PickUpManager : MonoBehaviour
{
    public static PickUpManager current;

    [SerializeField] List<GameObject> allPickupLocations;
    List<GameObject> freePickupLocations;

    [SerializeField] List<GameObject> pickups;

    [SerializeField] int spawnCoolDown;

    [HideInInspector]
    public AvailablePickups currentPickupPlayerOne;
    [HideInInspector]
    public AvailablePickups currentPickupPlayerTwo;

    public GameObject minePrefab;

    GameObject playerOne;

    GameObject playerTwo;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("Player1");

        playerTwo = GameObject.FindGameObjectWithTag("Player2");

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
                //Debug.LogError("There are no spawn locations available on the list");
            }
        }
    }

    void SpawnPickUp()
    {
        int randomPickup = Random.Range(0, pickups.Count);
        int randomLocation = Random.Range(0, freePickupLocations.Count);

        Instantiate(pickups[randomPickup], freePickupLocations[randomLocation].transform);

        freePickupLocations.Remove(freePickupLocations[randomLocation]);
    }

    public void FreeUpSpawnLocation(GameObject location)
    {
        freePickupLocations.Add(location);
    }

    public void UsePickUpPlayerOne()
    {
        if (currentPickupPlayerOne != AvailablePickups.Empty)
        {
            if (currentPickupPlayerOne == AvailablePickups.Mine)
            {
                SpawnMine(1);
            }

            if(currentPickupPlayerOne == AvailablePickups.DisableRadar)
            {
                DisableRadar(1);
            }
        }

        currentPickupPlayerOne = AvailablePickups.Empty;
    }

    public void UsePickUpPlayerTwo()
    {
        if (currentPickupPlayerOne != AvailablePickups.Empty)
        {
            if (currentPickupPlayerTwo == AvailablePickups.Mine)
            {
                SpawnMine(2);
            }

            if (currentPickupPlayerTwo == AvailablePickups.DisableRadar)
            {
                DisableRadar(2);
            }
        }

        currentPickupPlayerTwo = AvailablePickups.Empty;
    }

    void SpawnMine(int playerNumber)
    {
        if(playerNumber == 1)
        {
            GameObject mine = Instantiate(minePrefab, playerOne.transform);

            mine.transform.parent = null;

            mine.tag = "Player1";
        }

        if (playerNumber == 2)
        {
            GameObject mine = Instantiate(minePrefab, playerTwo.transform);

            mine.transform.parent = null;

            mine.tag = "Player2";
        }
    }

    void DisableRadar(int playerNumber)
    {

    }
}
    