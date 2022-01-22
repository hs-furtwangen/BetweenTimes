using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using DebugHelper;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class Quest : MonoBehaviour
{
    public Task[] tasks;
    [SerializeField] private string token;
    private bool _complete;
    private Questcontroller
    public bool Complete
    {
        get => _complete;
        set => _complete = value;
    }

    public string Token => token;
    
    public Task GetTask(string token)
    {
        foreach (var task in tasks)
        {
            if (task.Token == token)
                return task;
        }

        return null;
    }

    public bool ContainsTask(string token)
    {        
        foreach (var task in tasks)
        {
            if (task.Token == token)
                return true;
        }

        return false;
    }
    
    public bool MarkTaskAsComplete(string token)
    {
        if (!ContainsTask(token))
        {
            DebugColored.Log(true, Color.magenta, this, "No task with token " + token + " found in quest with token "+this.token);
            return false;
        }
        
        Task requested = GetTask(token);
        if (!requested.Complete)
        {
            requested.Complete = true;
            DebugColored.Log(true, Color.magenta, this, "Successfully marked " + token + " in quest with token "+this.token);
            
            return true;
        }
        else
        {
            DebugColored.Log(true, Color.magenta, this, "Task " + token + " already complete");
            return false;
        }
    }

    private void CheckIfQuestComplete()
    {
        foreach (var task in tasks)
        {
            if (!task.Complete)
                return;
        }

        _complete = true;
        QuestCompleted();
    }
    
    public void QuestCompleted()
    {
    }
}
