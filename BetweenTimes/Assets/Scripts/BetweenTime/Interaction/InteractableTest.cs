using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTest : Interactable
{
    void Start()
    {
        EventHoverEnter.AddListener(() =>
        {
            Debug.Log("Hover Enter");
        });
        EventHoverExit.AddListener(() =>
        {
            Debug.Log("Hover Exit");
        });
        EventInteract.AddListener(interactor =>
        {
            Debug.Log("Interact");
        });
    }
}
