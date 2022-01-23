using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayerCameraMovement : MonoBehaviour
{
    [SerializeField] public Camera Camera;
    [SerializeField] Transform cameraOrientationTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform faceOrientationTransform;

    [SerializeField] private float distance;
    void Update()
    {

        // float dist = Vector3.Distance(playerTransform.position, cameraOrientationTransform.position);
        //
        // if (dist >= distance)
        // {
        //
        //     cameraOrientationTransform.position = Vector3.Slerp(cameraOrientationTransform.position,
        //         playerTransform.position, Time.deltaTime);
        //    
        // }
        // cameraOrientationTransform.LookAt(playerTransform);
        // cameraOrientationTransform.Translate(
        //     (playerTransform.position.x - Camera.transform.position.x) * 0.9f,
        //     0f,
        //     (playerTransform.position.z - Camera.transform.position.z) * 0.9f
        // );
        // cameraOrientationTransform.Rotate(0f, (playerTransform.eulerAngles.y - cameraOrientationTransform.eulerAngles.y) * 0.9f, 0f);
        // Camera.transform.Rotate((faceOrientationTransform.eulerAngles.x - Camera.transform.eulerAngles.x) * 0.9f, 0f, 0f);
    }
}
