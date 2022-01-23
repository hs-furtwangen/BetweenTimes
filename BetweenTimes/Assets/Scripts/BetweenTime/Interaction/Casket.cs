using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casket : Interactable
{
    bool isOpen;

    protected override void OnEnable()
    {
        base.OnEnable();
        EventInteract.AddListener(Open);
    }

    void Open(Interactor interactor)
    {
        if (interactor.Collected)
        {
            Debug.Log("Opened casket");
            interactor.Collected = null;
        }
    }
}
