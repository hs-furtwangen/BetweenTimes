using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using DebugHelper;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Serializable]
public class Quest
{
    public Task[] tasks;
    [SerializeField] private string token;
    [SerializeField] private bool _complete;
    private Questcontroller _questcontroller;
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
        DebugColored.Log(true, Color.magenta, "[Quest]", "Try to mark token "+token);

        if (!ContainsTask(token))
        {
            DebugColored.Log(true, Color.magenta, "[Quest]", "No task with token " + token + " found in quest with token "+this.token);
            return false;
        }
        
        Task requested = GetTask(token);
        if (!requested.Complete)
        {
            requested.Complete = true;
            DebugColored.Log(true, Color.magenta, "[Quest]", "Successfully marked " + token + " in quest with token "+this.token);
            CheckIfQuestComplete();
            return true;
        }
        else
        {
            DebugColored.Log(true, Color.magenta, "[Quest]", "Task " + token + " already complete");
            return false;
        }
    }

    private bool CheckIfQuestComplete()
    {
        if (_complete)
            return true;
        
        foreach (var task in tasks)
        {
            if (!task.Complete)
                return false;
        }

        _complete = true;
        return true;
    }

    public void SetComplete()
    {
        if (_complete)
            return;
        
        foreach (var task in tasks)
        {
            task.Complete = true;
        }

        _complete = true;
    }
    
    public void ResetQuest()
    {
        ResetTasks();
        _complete = false;
    }
    
    public void ResetTasks()
    {
        foreach (var task in tasks)
        {
            task.Complete = false;
        }
    }
}
