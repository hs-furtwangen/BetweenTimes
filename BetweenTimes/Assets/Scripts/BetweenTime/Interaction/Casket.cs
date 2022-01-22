using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casket : Interactable
{
    bool isOpen;
    void OnEnable()
    {
        EventInteract.AddListener(Open);
    }

    void OnDisable()
    {
        EventInteract.RemoveListener(Open);
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
