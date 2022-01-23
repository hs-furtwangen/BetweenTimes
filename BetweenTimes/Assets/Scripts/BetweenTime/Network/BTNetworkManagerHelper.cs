using System.Collections;
using System.Collections.Generic;
using BetweenTime.Network;
using DebugHelper;
using TMPro;
using UnityEngine;

public class BTNetworkManagerHelper : MonoBehaviour
{
    [SerializeField] private BTCustomNetworkManager _networkManager;
    public TextMeshProUGUI ipTextMesh;

    private string ipv4 = "";

    public void ConnectAsClient()
    {
        ipv4 = ipTextMesh.text;

        if (!string.IsNullOrEmpty(ipv4))
            _networkManager.networkAddress = ipv4;
        else
            _networkManager.networkAddress = "localhost";    
        
        DebugColored.Log(true, Color.green, this, "Connect as client to "+_networkManager.networkAddress);
        _networkManager.StartClient();
    }

    public void ConnectAsHost()
    {
        DebugColored.Log(true, Color.green, this, "Connect as Host");
        _networkManager.StartHost();
    }
}
