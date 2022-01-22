using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInput : MonoBehaviour
{
    float rotationSpeed = 1f;

    private void Awake()
    {
        Physics.queriesHitTriggers = true;
        Debug.Log("yup");
    }

    private void OnMouseDrag()
    {
        Debug.Log("testing");
    }
}
