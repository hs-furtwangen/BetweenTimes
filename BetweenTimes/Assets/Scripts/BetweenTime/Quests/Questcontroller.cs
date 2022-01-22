using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Questcontroller : MonoBehaviour
{
    public bool reset;
    public Quests[] quest;
    private int currentQuest;

    private void OnEnable()
    {
        reset = false;
        currentQuest = 0;

        for (int i = 0; i < quest.Length; i++)
        {
            quest[i].EventQuestComplete += QuestPartComplete;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < quest.Length; i++)
        {
            quest[i].EventQuestComplete -= QuestPartComplete;
        }
    }

    public void QuestPartComplete()
    {
        if (currentQuest < quest.Length - 1)
        {
            Debug.Log("Quest geschafft");
            currentQuest += 1;
        }
        else
        {
            Debug.Log("Spiel geschafft");
        }
    }
    public void Restart()
    {
        if (reset)
        {
            for (int i = 0; i < quest.Length; i++)
            {
                quest[i].reset = true;
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
