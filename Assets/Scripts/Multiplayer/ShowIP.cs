using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowIP : MonoBehaviour
{
    public TMP_Text ipText;

    void Start()
    {
        ipText.text = "IP do Host: " + NetworkHelper.GetLocalIPAddress();
    }
}
