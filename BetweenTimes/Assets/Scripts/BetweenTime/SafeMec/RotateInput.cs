using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInput : MonoBehaviour
{
    private Vector3 mousePosition;

    float rSpeed = 1f; // Scale. Speed of the movement
    Vector3 pointingTarget;

    private void Awake()
    {
        Debug.Log("yup");
    }

    private void OnMouseDrag()
    {

        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition - new Vector3(0, 0, Camera.main.transform.position.z));
        mouseToWorld.z = 0f;
        Vector3 difference = mouseToWorld - transform.position;
        difference.Normalize();

        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle - 90);
    }
}
