using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenTime.Network.Player;
using BetweenTime.Player;

public class Interactor : MonoBehaviour
{
    [SerializeField] public BTPlayerMovement Movement;
    [SerializeField] Transform faceOrientationTransform;
    public BTPlayerInput Input { get => Movement.Input; }
    public Collectable Collected;
    [SerializeField] bool showDebug;
    Interactable hovered;

    public void Focus(Interactable interactable)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Movement.enabled = false;
        Movement.LookAt(interactable.transform);
    }

    public void LoseFocus()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Movement.enabled = true;
    }

    void Start()
    {
        LoseFocus();
    }

    void OnEnable()
    {
        Input.EventFireDown.AddListener(Interact);
    }

    void OnDisable()
    {
        Input.EventFireDown.RemoveListener(Interact);
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(faceOrientationTransform.position, faceOrientationTransform.forward, out hit, 2.5f))
        {
            if (showDebug) Debug.Log("Raycast did hit");
            Interactable newHovered = hit.collider.GetComponent<Interactable>();
            if (newHovered != hovered)
            {
                hovered?.EventHoverExit.Invoke();
                newHovered?.EventHoverEnter.Invoke();
            }
            hovered = newHovered;
        }
        else
        {
            if (showDebug) Debug.Log("Raycast did not hit");
            hovered?.EventHoverExit.Invoke();
            hovered = null;
        }
    }

    void Interact()
    {
        if (hovered)
        {
            hovered.EventInteract.Invoke(this);
            if (hovered is Collectable)
            {
                if (Collected == null)
                {
                    Collected = hovered as Collectable;
                    Collected.gameObject.SetActive(false);
                }
                else if (showDebug) Debug.Log("Inventory full!", this);
            }
        }
        else if (Collected)
        {
            Collected.gameObject.SetActive(true);
            Collected.EventUse.Invoke(this);
            Collected = null;
        }
    }
}
