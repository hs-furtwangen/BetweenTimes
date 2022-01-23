using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum TokenType
{
    Task,
    Quest
}

public class QuestObserver : MonoBehaviour
{

    private Questcontroller _questcontroller;
    [SerializeField] private string tokenToObserve;
    
    [SerializeField] private TokenType tokenType;

    public UnityEvent EventOnObserved;
    
    private void Start()
    {
        if(_questcontroller == null) _questcontroller = Questcontroller.GetInstance();
        
        if(tokenType == TokenType.Quest)
            _questcontroller.EventOnQuestCompleted.AddListener(OnCompleted);
        else if(tokenType == TokenType.Task)
            _questcontroller.EventOnTaskCompleted.AddListener(OnCompleted);
    }


    public  void OnCompleted(string token)
    {
        if (token != tokenToObserve)
            return;
        
        EventOnObserved?.Invoke();
        ExecuteOnCompletion();
    }

    protected virtual void ExecuteOnCompletion()
    {
        
    }
}
