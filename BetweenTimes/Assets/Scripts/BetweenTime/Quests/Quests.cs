using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Quests : MonoBehaviour
{

    UnityEvent m;
    public Tasks[] task;
    private int currentTask;

    public event Action EventQuestComplete;
    public bool reset;

    private void OnEnable()
    {
        reset = false;
        m = new UnityEvent();
        currentTask = 0;
        for (int i = 0; i < task.Length; i++)
        {
            task[i].EventTaskComplete += QuestTaskComplete;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < task.Length; i++)
        {
            task[i].EventTaskComplete -= QuestTaskComplete;
        }
    }

    private void QuestTaskComplete()
    {

        if (currentTask < task.Length - 1)
        {
            currentTask += 1;
        }
        else
        {
            QuestComplete();
        }
    }
    private void QuestComplete()
    {
        EventQuestComplete?.Invoke();
    }
    public void Restart()
    {
        if (reset)
        {
            for (int i = 0; i < task.Length; i++)
            {
                task[i].reset = true;
            }
            this.enabled = false;
            this.enabled = true;
        }
    }



    private void Update()
    {
        Restart();
    }
}
