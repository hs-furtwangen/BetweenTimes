using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableCheck : Interactable
{
    bool isOpen;

    [SerializeField] private Collectable collectableToCheck;

    [SerializeField] private bool _consume;
    
    public UnityEvent EventAcceptedInteraction;

    
    protected override void OnEnable()
    {
        base.OnEnable();
        EventInteract.AddListener(Open);
    }

    void Open(Interactor interactor)
    {
        if (interactor.Collected == collectableToCheck)
        {
            Debug.Log("Opened casket");
            if(_consume) interactor.Collected = null;
            EventAcceptedInteraction?.Invoke();
        }
    }
}
