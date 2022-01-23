using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    [SerializeField] private string token;
    [SerializeField] private bool complete = false;

    public bool Complete
    {
        get => complete;
        set => complete = value;
    }

    public string Token => token;

}
