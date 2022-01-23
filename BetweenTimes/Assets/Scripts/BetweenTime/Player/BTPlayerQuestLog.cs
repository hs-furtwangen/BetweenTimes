using System;
using System.Collections;
using System.Collections.Generic;
using DebugHelper;
using Mirror;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class BTPlayerQuestLog : NetworkBehaviour
{
    private Questcontroller _questcontroller;
    public void Start()
    {
        _questcontroller = Questcontroller.GetInstance();

        if (isLocalPlayer)
        {
            _questcontroller.EventOnReset.AddListener(OnResetQuests);
            _questcontroller.EventOnTaskCompleted.AddListener(OnTaskComplete);
            _questcontroller.EventOnAllQuestsFinished.AddListener(OnAllQuestsComplete);
            _questcontroller.EventOnQuestCompleted.AddListener(OnQuestComplete);
            DebugColored.Log(true, Color.magenta, this, "Register QuestController");

        }
    }

    public void OnQuestComplete(string token)
    {
        if (!isLocalPlayer)
            return;
        
        CmdQuestComplete(token);
    }

    public void OnTaskComplete(string token)
    {
        if (!isLocalPlayer)
            return;
        
        CmdTaskComplete(token);
    }

    public void OnAllQuestsComplete()
    {
        if (!isLocalPlayer)
            return;
        
        CmdAllQuestsComplete();
    }

    public void OnResetQuests()
    {
        if (!isLocalPlayer)
            return;
        
        CmdOnResetQuests();
    }

    [Command]
    public void CmdQuestComplete(string token)
    {
        RpcQuestComplete(token);
    }

    [ClientRpc(includeOwner = false)]
    public void RpcQuestComplete(string token)
    {
        DebugColored.Log(true, Color.magenta, "[Client]",this, "RpQuestComplete "+token);
        _questcontroller.SetQuestComplete(token);
    }

    [Command]
    public void CmdTaskComplete(string token)
    {
        RpcTaskComplete(token);
        
    }
    
    [ClientRpc(includeOwner = false)]
    public void RpcTaskComplete(string token)
    {
        DebugColored.Log(true, Color.magenta, "[Client]",this, "RpcTaskComplete "+token);
        _questcontroller.SetTaskComplete(token);
    }

    [Command]
    public void CmdAllQuestsComplete()
    {
        RpcAllQuestsComplete();
    }
    
    [ClientRpc(includeOwner = false)]
    public void RpcAllQuestsComplete()
    {
        DebugColored.Log(true, Color.magenta, "[Client]",this, "RpcAllQuests");
        _questcontroller.SetAllQuestsComplete();
    }

    [Command]
    public void CmdOnResetQuests()
    {
        RpcOnResetQuests(); 
    }
    
    [ClientRpc(includeOwner = false)]
    public void RpcOnResetQuests()
    {
        DebugColored.Log(true, Color.magenta, "[Client]",this, "RpcReset");
        _questcontroller.Reset();
    }
}