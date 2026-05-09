using UnityEngine;
using Unity.Netcode;
using TMPro;

public class MultiplayerMenu : MonoBehaviour
{

    //player counter
    [SerializeField] public TMP_Text PlayerCountText;
    //Buttons
    [SerializeField] public GameObject HostButton;
    [SerializeField] public GameObject ClientButton;
    [SerializeField] public GameObject ServerButton;

    void Update()
    {
        // Find all objects with the "Player" tag every frame
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        int playerCount = players.Length;

        // Update TMP text
        PlayerCountText.text = "Players: " + playerCount;
    }


    public void StartHost()//starts game as both server and client
    {
        NetworkManager.Singleton.StartHost();
        HostButton.SetActive(false);
        ClientButton.SetActive(false);
        ServerButton.SetActive(false);
    }

    public void StartClient()//starts game as client that connect to host ort server
    {
        NetworkManager.Singleton.StartClient();
        HostButton.SetActive(false);
        ClientButton.SetActive(false);
        ServerButton.SetActive(false);
    }
    public void StartServer()//starts game as a server only
    {
        NetworkManager.Singleton.StartServer();
        HostButton.SetActive(false);
        ClientButton.SetActive(false);
        ServerButton.SetActive(false);
    }

}
