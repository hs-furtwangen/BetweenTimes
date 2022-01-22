using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenTime.Network.Player;
using DebugHelper;

public class Interactor : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool showDebug;
    [SerializeField] private Color debugColor;

    private Camera camera;
    private Interactable hovered;
    private Interactable collected;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponentInChildren<Camera>();
        var playerInput = GetComponent<BTPlayerInput>();
        playerInput.EventOnFireDown.AddListener(OnFire);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, camera.transform.forward, out hit, 2))
        {
            DebugColored.Log(showDebug, debugColor, this, "Did hit");
            var newHovered = hit.rigidbody.GetComponent<Interactable>();
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

    void OnFire()
    {
        if (hovered)
        {
            hovered.EventInteract.Invoke();
            if (hovered.IsCollectable)
            {
                if (collected == null)
                {
                    hovered.gameObject.SetActive(false);
                    collected = hovered;
                }
                else DebugColored.Log(showDebug, debugColor, this, "Inventory full!");
            }
        }
        else
        {
            if (collected)
            {
                collected.gameObject.SetActive(true);
                collected.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward);
                collected = null;
            }
        }
    }
}
