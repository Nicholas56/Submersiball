﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//Tutorial Used: https://www.youtube.com/watch?v=s2ypWi553nY

public class PlayerSpawnSystem : NetworkBehaviour
{
    [SerializeField] GameObject playerPrefab = null;

    static List<Transform> spawnPoints = new List<Transform>();
    int nextIndex = 0;

    public static void AddSpawnPoint(Transform transform)
    {
        spawnPoints.Add(transform);
        spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }
    public static void RemoveSpawnPoint(Transform transform) => spawnPoints.Remove(transform);

    public override void OnStartServer() => NetworkManagerLobby.OnServerReadied += SpawnPlayer;
    [ServerCallback]
    private void OnDestroy() => NetworkManagerLobby.OnServerReadied -= SpawnPlayer;
    
    [Server]
    public void SpawnPlayer(NetworkConnection conn)
    {
        Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);
        if (spawnPoint == null)
        {
            Debug.LogError($"Missing spawn point for player {nextIndex}");
            return;
        }

        GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position,spawnPoints[nextIndex].rotation);
        NetworkServer.AddPlayerForConnection(conn,playerInstance);//Changed from: NetworkServer.Spawn(playerInstance, conn);

        nextIndex++;
    }
}