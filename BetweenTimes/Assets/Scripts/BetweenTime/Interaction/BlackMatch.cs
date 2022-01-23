using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMatch : Collectable
{
    protected override void OnEnable()
    {
        base.OnEnable();
        EventUse.AddListener(ThrowMatch);
    }

    void ThrowMatch(Interactor interactor)
    {
        Debug.Log("Throw");
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.MovePosition(interactor.transform.position + interactor.transform.forward);
        rigidbody.AddForce(interactor.transform.forward + interactor.transform.up);
    }
}
