using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    UnityEvent EventHover;
    UnityEvent EventInteract;

    void Awake()
    {
        if (gameObject.GetComponent<Collider>() == null) gameObject.AddComponent<BoxCollider>();
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
