using UnityEngine;
using Mirror;

public class NetworkHost : MonoBehaviour
{
    public void StartHost()
    {
        Debug.Log("Iniciando sessão como Host...");
        NetworkManager.singleton.StartHost();
        Debug.Log("Sessão iniciada no IP: " + NetworkHelper.GetLocalIPAddress());
    }
}
