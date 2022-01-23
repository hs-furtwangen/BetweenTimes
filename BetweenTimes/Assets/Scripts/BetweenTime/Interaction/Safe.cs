using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Safe : Interactable
{
    [SerializeField] Transform wheelTransform;
    public UnityEvent EventOpen;
    bool isOpen;
    List<int> input = new List<int>();
    int[] code = { 14, 6, 17 };
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
        int value = Mathf.FloorToInt(wheelTransform.eulerAngles.z / 18f);
        if (showDebug) Debug.Log("Submitted " + value);
        input.Add(value);
        if (input.Count >= 3)
        {
            int index = 0;
            if (input.FindIndex(value => value != code[index++]) >= 0)
            {
                Debug.Log("Wrong combination");
            }
            else
            {
                isOpen = true;
                EventOpen?.Invoke();
            }
            input.Clear();
            _interactor.Input.EventMouse.RemoveListener(RotateWheel);
            _interactor.Input.EventFireDown.RemoveListener(Submit);
            _interactor.LoseFocus();
            _interactor = null;
        }
    }

    void RotateWheel(float x, float y)
    {
        Vector3 positionToScreen = _interactor.Movement.Camera.WorldToScreenPoint(wheelTransform.position);
        Vector3 difference = Input.mousePosition - positionToScreen;
        difference.Normalize();
        float angle = Mathf.Floor(Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg / 18f) * 18f;
        wheelTransform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        Debug.Log(Mathf.FloorToInt(wheelTransform.eulerAngles.z / 18f));
    }
}
