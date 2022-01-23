using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
public abstract class Interactable : MonoBehaviour
{
    public UnityEvent EventHoverEnter = new UnityEvent();
    public UnityEvent EventHoverExit = new UnityEvent();
    public UnityEvent<Interactor> EventInteract = new UnityEvent<Interactor>();
    Outline outline;

    void Awake()
    {
        if (GetComponent<Collider>() == null) gameObject.AddComponent<BoxCollider>();
        outline = GetComponent<Outline>();
        if (!outline) outline = gameObject.AddComponent<Outline>();
        outline.OutlineWidth = 10;
        outline.enabled = false;
        EventHoverEnter.AddListener(() => outline.enabled = true);
        EventHoverExit.AddListener(() => outline.enabled = false);
    }
}
