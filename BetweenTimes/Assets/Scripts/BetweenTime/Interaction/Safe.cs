using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenTime.Network.Player;

public class Safe : Interactable
{
    [SerializeField] GameObject wheel;
    Camera camera;
    bool isOpen;
    Interactor interactor;
    BTPlayerInput playerInput;

    void OnEnable()
    {
        EventHoverEnter.AddListener(() =>
        {
            Debug.Log("Hover Enter");
        });
        EventHoverExit.AddListener(() =>
        {
            Debug.Log("Hover Exit");
        });
        EventInteract.AddListener(EnterCombination);
    }

    void OnDisable() {
        EventInteract.RemoveListener(EnterCombination);
    }

    void EnterCombination(Interactor interactor)
    {
        Debug.Log("Interact");
        interactor.Focus(this);
        this.interactor = interactor;
        camera = interactor.GetComponentInChildren<Camera>();
        playerInput = interactor.GetComponent<BTPlayerInput>();
        playerInput.EventOnMouse.AddListener(RotateWheel);
        playerInput.EventOnFireDown.AddListener(Submit);
    }

    void Submit()
    {
        Debug.Log("Submit");
        isOpen = true;
        interactor.LoseFocus();
        playerInput.EventOnFireDown.RemoveListener(Submit);
        playerInput.EventOnMouse.RemoveListener(RotateWheel);
        interactor = null;
    }

    void RotateWheel(float x, float y)
    {
        Vector3 mouseToWorld = camera.ScreenToWorldPoint(Input.mousePosition - new Vector3(0, 0, camera.transform.position.z));
        mouseToWorld.z = 0f;
        Vector3 difference = mouseToWorld - transform.position;
        difference.Normalize();

        float angle = Mathf.Round(Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg / 18) * 18;
        wheel.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
    }
}
