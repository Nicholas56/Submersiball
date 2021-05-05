using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Collections.Generic;
using TMPro;

/*
 * tutorial used: https://www.youtube.com/watch?v=WMJS7sVp2FQ
*/

public class NetworkGamePlayerLobby : NetworkBehaviour
{    
    [SyncVar]
    string displayName = "Loading...";


    NetworkManagerLobby room;
    NetworkManagerLobby Room
    {
        get
        {
            if(room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        Room.GamePlayers.Add(this);
    }
    public override void OnStopClient()
    {
        Room.GamePlayers.Remove(this);
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }
}
