using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTest : Interactable
{
    // Start is called before the first frame update
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
        EventInteract.AddListener(() =>
        {
            Debug.Log("Interact");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
