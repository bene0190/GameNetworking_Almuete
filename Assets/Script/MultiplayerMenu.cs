using UnityEngine;
using Unity.Netcode;

public class MultiplayerMenu : MonoBehaviour
{

    public void StartHost()//starts game as both server and client
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()//starts game as client that connect to host ort server
    {
        NetworkManager.Singleton.StartClient();
    }
    public void StartServer()//starts game as a server only
    {
        NetworkManager.Singleton.StartServer();
    }

}
