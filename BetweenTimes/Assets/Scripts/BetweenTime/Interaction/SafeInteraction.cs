using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenTime.Network.Player;

public class SafeInteraction : Interactable
{
    [SerializeField] GameObject wheel;
    Camera camera;
    bool isOpen;
    Interactor interactor;

    // Start is called before the first frame update
    void Start()
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

    void EnterCombination(Interactor interactor)
    {
        Debug.Log("Interact");
        interactor.Focus(this);
        this.interactor = interactor;
        camera = interactor.GetComponentInChildren<Camera>();
        interactor.GetComponent<BTPlayerInput>().EventOnFireDown.AddListener(Submit);
    }

    void Submit()
    {
        Debug.Log("Submit");
        isOpen = true;
        interactor.LoseFocus();
        interactor.GetComponent<BTPlayerInput>().EventOnFireDown.RemoveListener(Submit);
        interactor = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactor)
        {
            Vector3 mouseToWorld = camera.ScreenToWorldPoint(Input.mousePosition - new Vector3(0, 0, camera.transform.position.z));
            mouseToWorld.z = 0f;
            Vector3 difference = mouseToWorld - transform.position;
            difference.Normalize();

            float angle = Mathf.Round(Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg / 18) * 18;
            wheel.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
        }
    }
}
