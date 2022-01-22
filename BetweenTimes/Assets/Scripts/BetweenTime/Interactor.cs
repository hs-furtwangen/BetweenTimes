using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenTime.Network.Player;

public class Interactor : MonoBehaviour
{
    private Camera camera;
    private Interactable hoveredInteractable;

    // Start is called before the first frame update
    void Start()
    {
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
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            var newHoveredInteractable = hit.rigidbody.GetComponent<Interactable>();
            if (newHoveredInteractable != hoveredInteractable)
            {
                hoveredInteractable?.EventHoverExit.Invoke();
                newHoveredInteractable?.EventHoverEnter.Invoke();
            }
            hoveredInteractable = newHoveredInteractable;
        }
        else
        {
            hoveredInteractable?.EventHoverExit.Invoke();
            hoveredInteractable = null;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    void OnFire()
    {
        if (hoveredInteractable)
            hoveredInteractable.EventInteract.Invoke();
    }
}
