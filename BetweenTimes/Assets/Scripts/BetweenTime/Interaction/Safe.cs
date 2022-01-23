using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenTime.Network.Player;

public class Safe : Interactable
{
    [SerializeField] GameObject wheel;
    bool isOpen;
    Interactor _interactor;

    protected override void OnEnable()
    {
        base.OnEnable();
        EventInteract.AddListener(EnterCombination);
    }

    void EnterCombination(Interactor interactor)
    {
        if (showDebug) Debug.Log("Enter combination");
        _interactor = interactor;
        _interactor.Focus(this);
        _interactor.Input.EventMouse.AddListener(RotateWheel);
        _interactor.Input.EventFireDown.AddListener(Submit);
    }

    void Submit()
    {
        if (showDebug) Debug.Log("Submit");
        isOpen = true;
        _interactor.Input.EventFireDown.RemoveListener(Submit);
        _interactor.Input.EventMouse.RemoveListener(RotateWheel);
        _interactor.LoseFocus();
        _interactor = null;
    }

    void RotateWheel(float x, float y)
    {
        Vector3 mouseToWorld = _interactor.Movement.Camera.ScreenToWorldPoint(Input.mousePosition - new Vector3(0f, 0f, _interactor.Movement.Camera.transform.position.z));
        mouseToWorld.z = 0f;
        Vector3 difference = mouseToWorld - transform.position;
        difference.Normalize();
        float angle = Mathf.Round(Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg / 18f) * 18f;
        wheel.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
    }
}
