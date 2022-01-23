using System;
using System.Collections;
using System.Collections.Generic;
using DebugHelper;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Serializable]
public class Questcontroller : MonoBehaviour
{
    #region Singleton

    private static Questcontroller instance;
    public static Questcontroller GetInstance()
    {
        return instance;
    }
    
    #endregion Singleton
    
    public Quest[] quests;

    [Header("Debug")] 
    [SerializeField] private bool showDebug;
    [SerializeField] private Color debugColor;
    
    #region Events
    public UnityEvent<string> EventOnQuestCompleted = new UnityEvent<string>();
    public UnityEvent<string> EventOnTaskCompleted = new UnityEvent<string>();
    public UnityEvent EventOnAllQuestsFinished;
    public UnityEvent EventOnReset;
    #endregion Events

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    
    public void OnTaskToken(string token)
    {
        DebugColored.Log(showDebug,debugColor,this, "Task complete "+token);
        EventOnTaskCompleted?.Invoke(token);
    }
    public void OnQuestComplete(string token)
    {
        DebugColored.Log(showDebug,debugColor,this, "Quest complete "+token);
        EventOnQuestCompleted?.Invoke(token);
    }
    public void OnAllQuestsComplete()
    {
        DebugColored.Log(showDebug,debugColor,this, "All quests complete!");
        EventOnAllQuestsFinished?.Invoke();
    }
    
    public void Reset()
    {
        DebugColored.Log(showDebug,debugColor,this, "Reset quests!");
        foreach (var quest in quests)
        {
            quest.ResetQuest();
        }
    }
    
    /// <summary>
    /// For the network to call on this one, if the other player has finished a task
    /// </summary>
    /// <param name="token"></param>
    public void SetTaskComplete(string token)
    {
        bool isNotCompleted = false;
        foreach (var q in quests)
        {
            if (q.Complete)
                continue;
            
            bool hasTask = false;
            if (q.MarkTaskAsComplete(token))
            {
                OnTaskToken(token);
                hasTask = true;
            }

            if (q.Complete)
            {
                OnQuestComplete(q.Token);
            }
            else
                isNotCompleted = true;

            if (hasTask) return;
        }
        
        if(!isNotCompleted)
            OnAllQuestsComplete();
    }
    
    public void SetQuestComplete(string token)
    {
        foreach (var q in quests)
        {
            if (q.Token == token)
            {
                if (!q.Complete)
                {
                    q.SetComplete();
                    OnQuestComplete(token);
                    return;
                }
                else
                {
                    DebugColored.Log(showDebug,debugColor,this, "Quest already completed: "+token);
                    return;
                }
            }
        }
    }

    public void SetAllQuestsComplete()
    {
        foreach (var quest in quests)
        {
            quest.SetComplete();
        }
    }
}
