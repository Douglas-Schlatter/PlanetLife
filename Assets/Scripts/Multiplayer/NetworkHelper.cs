using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NetworkHelper : MonoBehaviour
{
    public static string GetLocalIPAddress()
    {
        string localIP = "127.0.0.1"; // Fallback padrão

        try
        {
            foreach (var netInterface in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (netInterface.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = netInterface.ToString();
                    break;
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Erro ao obter IP local: " + ex.Message);
        }

        return localIP;
    }
}
