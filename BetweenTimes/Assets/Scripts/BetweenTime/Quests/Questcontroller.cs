using System;
using System.Collections;
using System.Collections.Generic;
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
    
    public bool reset;
    public Quest[] quests;
    private int currentQuest;

    #region Events
    public UnityEvent<string> EventOnQuestPartCompleted = new UnityEvent<string>();
    public UnityEvent<string> EventOnTaskCompleted = new UnityEvent<string>();
    public UnityEvent EventOnAllQuestsFinished;
    public UnityEvent EventOnReset;
    #endregion Events
    
    private void OnEnable()
    {
        reset = false;
        currentQuest = 0;

        for (int i = 0; i < quests.Length; i++)
        {
           // quests[i].EventQuestComplete += OnQuestComplete;
          //  quests[i].EventTaskComplete += TaskComplete;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < quests.Length; i++)
        {
          //  quests[i].EventQuestComplete -= OnQuestComplete;
          //  quests[i].EventTaskComplete -= TaskComplete;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void TaskComplete(string token)
    {
        foreach (var quest in quests)
        {
            if (quest.ContainsTask(token))
            {
                
            }
        }
        
       // CheckIfQuestComplete();
        EventOnTaskCompleted?.Invoke(token);
    }
    
    public void OnQuestComplete(string token)
    {
        if (currentQuest < quests.Length - 1)
        {
            Debug.Log("Quest geschafft "+token);
            EventOnQuestPartCompleted?.Invoke(token);
            currentQuest += 1;
        }
        else
        {
            Debug.Log("Spiel geschafft");
            EventOnAllQuestsFinished?.Invoke();
        }
    }

    public void Restart()
    {
        if (reset)
        {
            for (int i = 0; i < quests.Length; i++)
            {
            //    quests[i].reset = true;
            }
            this.enabled = false;
            this.enabled = true;
        }
        
        EventOnReset?.Invoke();
    }
    
    #region Entry points for setting Tasks // network workaround  
    /// <summary>
    /// For the network to call on this one, if the other player has finished a task
    /// </summary>
    /// <param name="token"></param>
    public void SetTaskComplete(string token)
    {
        foreach (var q in quests)
        {
            foreach (var t in q.tasks)
            {
                if (t.Token == token)
                {
                  //  t.TaskComplete();
                }
            }
        }
    }
    

    public void SetQuestComplete(string token)
    {
        foreach (var q in quests)
        {
            if (q.Token == token)
            {
                q.QuestCompleted();
            }
        }
    }
    
    public void SetReset()
    {
        foreach (var quest in quests)
        {
            quest.QuestCompleted();
        }
    }

    public void SetAllQuestsComplete()
    {
        foreach (var quest in quests)
        {
            quest.QuestCompleted();
        }
    }
    private void Update()
    {
        Restart();
    }
    
    #endregion Entry points for setting Tasks // network workaround
}
