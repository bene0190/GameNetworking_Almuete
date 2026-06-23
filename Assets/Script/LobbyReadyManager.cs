using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class LobbyReadyManager : NetworkBehaviour
{
    [SerializeField] TMP_Text playerListText;
    [SerializeField] TMP_Text readyStatusText;
    [SerializeField] GameObject startGameButton;

    private Dictionary<ulong,bool> playerReadyStates = new Dictionary<ulong,bool>();
    //unassign long integer, bool for ready or not.

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
            foreach (ulong cliendId in NetworkManager.Singleton.ConnectedClientsIds)
            {
                playerReadyStates[cliendId] = false;
            }
            startGameButton.SetActive(IsServer);
        }
    }
    public override void OnNetworkDespawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback -= Singleton_OnClientDisconnectCallback;
        }
    }

    private void Singleton_OnClientDisconnectCallback(ulong obj)
    {
        
    }

    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        
    }

  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
