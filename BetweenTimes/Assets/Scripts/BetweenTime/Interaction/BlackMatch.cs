using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMatch : Collectable
{
    void OnEnable()
    {
        Debug.Log("Add Throw");
        EventUse.AddListener(ThrowMatch);
    }

    void OnDisable()
    {
        Debug.Log("Remove Throw");
        EventUse.RemoveListener(ThrowMatch);
    }

    void ThrowMatch(Interactor interactor)
    {
        Debug.Log("Throw");
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.MovePosition(interactor.transform.position + interactor.transform.forward);
        rigidbody.AddForce(interactor.transform.forward + interactor.transform.up);
    }
}
