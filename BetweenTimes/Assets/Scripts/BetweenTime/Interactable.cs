using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public UnityEvent EventHoverEnter;
    public UnityEvent EventHoverExit;
    public UnityEvent EventInteract;

    void Awake()
    {
        if (GetComponent<Collider>() == null) gameObject.AddComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
