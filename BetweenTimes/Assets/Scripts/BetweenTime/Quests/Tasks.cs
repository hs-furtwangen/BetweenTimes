using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tasks : MonoBehaviour
{

    public event Action EventTaskComplete;

    public bool reset;


    private bool complete = false;

    public void TaskComplete()
    {
        if (!complete)
        {
            complete = true;
            Debug.Log("Task geschafft");
            EventTaskComplete?.Invoke();
        }

    }

    public void Restart()
    {
        if (reset)
        {
            this.enabled = false;
            this.enabled = true;
            reset = false;
            complete = false;
        }
    }
}
