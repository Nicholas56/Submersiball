using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] GameObject landingPagePanel = null;
    [SerializeField] TMP_InputField ipAddressInputField = null;
    [SerializeField] Button joinButton = null;

    private void OnEnable()
    {
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
    }
    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
    }
    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;
        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();

        joinButton.interactable = false;
    }

    void HandleClientConnected()
    {
        joinButton.interactable = true;
        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }
    void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }
}
