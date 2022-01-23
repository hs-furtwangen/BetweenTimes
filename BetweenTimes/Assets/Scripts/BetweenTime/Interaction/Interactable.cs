using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteAlways]
public class Interactable : MonoBehaviour
{
    public UnityEvent EventHoverEnter = new UnityEvent();
    public UnityEvent EventHoverExit = new UnityEvent();
    public UnityEvent<Interactor> EventInteract = new UnityEvent<Interactor>();
    public UnityEvent<Interactor> EventCancleInteract= new UnityEvent<Interactor>();
    [SerializeField] protected bool showDebug;
    Outline outline;

    void Awake()
    {
        if (GetComponent<Collider>() == null) gameObject.AddComponent<BoxCollider>();
        outline = GetComponent<Outline>();
        if (!outline) 
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineWidth = 10f;
        }
        outline.enabled = false;
    }
    
    protected virtual void OnEnable()
    {
        EventHoverEnter.AddListener(() => outline.enabled = true);
        EventHoverExit.AddListener(() => outline.enabled = false);
        //EventInteract.AddListener(interactor => interactor.Input.EventRightDown.AddListener(EventCancleInteract.Invoke));
        if (showDebug) {
            EventHoverEnter.AddListener(() => Debug.Log("Hover enter"));
            EventHoverExit.AddListener(() => Debug.Log("Hover exit"));
            EventInteract.AddListener(interactor => Debug.Log("Interact"));
        }
    }
    
    
    void OnDisable()
    {
        EventHoverEnter.RemoveAllListeners();
        EventHoverExit.RemoveAllListeners();
        EventInteract.RemoveAllListeners();
    }
}
