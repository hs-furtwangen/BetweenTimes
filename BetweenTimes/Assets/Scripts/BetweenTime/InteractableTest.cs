using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTest : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        EventHover.AddListener(() =>
        {
            Debug.Log("Hover");
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
