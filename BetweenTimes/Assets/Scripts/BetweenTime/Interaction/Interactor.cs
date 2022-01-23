using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenTime.Network.Player;
using DebugHelper;
using BetweenTime.Player;

public class Interactor : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool showDebug;
    [SerializeField] private Color debugColor;

    private Camera camera;
    private BTPlayerMovement playerMovement;
    private Interactable hovered;
    public Collectable Collected;

    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        playerMovement = GetComponent<BTPlayerMovement>();
        BTPlayerInput playerInput = GetComponent<BTPlayerInput>();
        playerInput.EventOnFireDown.AddListener(OnFire);
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, camera.transform.forward, out hit, 2))
        {
            DebugColored.Log(showDebug, debugColor, this, "Did hit");
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
            DebugColored.Log(showDebug, debugColor, this, "Did not hit");
            hovered?.EventHoverExit.Invoke();
            hovered = null;
        }
    }

    public void Focus(Interactable interactable)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        camera.transform.LookAt(interactable.transform);
        playerMovement.enabled = false;
    }

    public void LoseFocus()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.enabled = true;
    }

    void OnFire()
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
                else DebugColored.Log(showDebug, debugColor, this, "Inventory full!");
            }
        }
        else
        {
            if (Collected)
            {
                Collected.gameObject.SetActive(true);
                Collected.EventUse.Invoke(this);
                Collected = null;
            }
        }
    }
}
