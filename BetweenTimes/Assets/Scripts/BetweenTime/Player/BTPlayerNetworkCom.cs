using System;
using System.Collections;
using System.Collections.Generic;
using BetweenTime.Network.Player;
using DebugHelper;
using Mirror;
using UnityEngine;

public class BTPlayerNetworkCom : NetworkBehaviour
{
    [SerializeField] private BTPlayerController playerController;
    [SerializeField] private BTPlayerInput inputModule;

    [Header("Debug")] 
    [SerializeField] private bool showDebug;
    [SerializeField] private Color debugColor;
    
    private void Start()
    {
        if (!isLocalPlayer)
            return;
        
        if(playerController == null) playerController = BTPlayerController.GetLocalPlayer();
        if(inputModule == null) inputModule = playerController.GetComponent<BTPlayerInput>();
        
        inputModule.EventFire.AddListener(OnFire);
        inputModule.EventFireUp.AddListener(OnFireUp);
        inputModule.EventFireDown.AddListener(OnFireDown);
    }

    [Client]
    public void OnFireDown()
    {
        DebugColored.Log(showDebug, debugColor, "[Client]", this, "OnFireDown");
        CmdOnFireDown();
    }
    [Client]
    public void OnFire()
    {
        DebugColored.Log(showDebug, debugColor, "[Client]", this, "OnFire");
        CmdOnFire();
    }
    [Client]
    public void OnFireUp()
    {
        DebugColored.Log(showDebug, debugColor, "[Client]", this, "OnFireUp");
        CmdOnFireUp();
    }

    [Command]
    public void CmdOnFireDown()
    {
        DebugColored.Log(showDebug, debugColor, "[Server]", this, "OnFireDown");
    }
    
    [Command]
    public void CmdOnFireUp()
    {
        DebugColored.Log(showDebug, debugColor, "[Server]", this, "OnFireUp");
    }    
    
    [Command]
    public void CmdOnFire()
    {
        DebugColored.Log(showDebug, debugColor, "[Server]", this, "OnFireDown");
    }
}
