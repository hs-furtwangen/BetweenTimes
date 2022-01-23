using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DistanceChecker : MonoBehaviour
{

    [SerializeField] private GameObject player;

    [SerializeField] private float maxDistance = 0.5f;

    public UnityEvent<Interactor> EventDisconnectFromInteractor = new UnityEvent<Interactor>();
    
    public void GiveInteractor(Interactor interactor)
    {
        if (player == null)
            player = interactor.gameObject;
    }

    public void Update()
    {
        if (player != null)
        {
            float curDist = Vector3.Distance(player.transform.position, transform.position);

            if (curDist >= maxDistance)
            {
                Disconnect();
            }
        }
    }

    public void Disconnect()
    {
        Interactor interactor = player.GetComponent<Interactor>();
        interactor.LoseFocus();
        player = null;
        EventDisconnectFromInteractor?.Invoke(interactor);
    }
}
