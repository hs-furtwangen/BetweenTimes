using System;
using System.Collections;
using System.Collections.Generic;
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
            _questcontroller.EventOnQuestPartCompleted.AddListener(OnQuestComplete);
        }
    }

    public void OnQuestComplete(string token)
    {
        CmdQuestComplete(token);
    }

    public void OnTaskComplete(string token)
    {
        CmdTaskComplete(token);
    }

    public void OnAllQuestsComplete()
    {
        CmdAllQuestsComplete();
    }

    public void OnResetQuests()
    {
        CmdOnResetQuests();
    }

    [Command]
    public void CmdQuestComplete(string token)
    {
        RpcQuestComplete(token);
    }

    [ClientRpc]
    public void RpcQuestComplete(string token)
    {
        _questcontroller.SetQuestComplete(token);
    }

    [Command]
    public void CmdTaskComplete(string token)
    {
        RpcTaskComplete(token);
        
    }
    
    [ClientRpc]
    public void RpcTaskComplete(string token)
    {
        _questcontroller.SetTaskComplete(token);
    }

    [Command]
    public void CmdAllQuestsComplete()
    {
        RpcAllQuestsComplete();
    }
    
    [ClientRpc]
    public void RpcAllQuestsComplete()
    {
        _questcontroller.SetAllQuestsComplete();
    }

    [Command]
    public void CmdOnResetQuests()
    {
        RpcOnResetQuests(); 
    }
    
    [ClientRpc]
    public void RpcOnResetQuests()
    {
        _questcontroller.SetReset();
    }
}