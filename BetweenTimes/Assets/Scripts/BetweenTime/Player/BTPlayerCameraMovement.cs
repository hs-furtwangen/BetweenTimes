using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayerCameraMovement : MonoBehaviour
{
    [SerializeField] public Camera Camera;
    [SerializeField] Transform cameraOrientationTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform faceOrientationTransform;

    void Update() {
        cameraOrientationTransform.Translate(
            (playerTransform.position.x - Camera.transform.position.x) * 0.9f,
            0f,
            (playerTransform.position.z - Camera.transform.position.z) * 0.9f
        );
        cameraOrientationTransform.Rotate(0f, (playerTransform.rotation.y - cameraOrientationTransform.rotation.y) * 0.9f, 0f);
        Camera.transform.Rotate((faceOrientationTransform.rotation.x - Camera.transform.rotation.x) * 0.9f, 0f, 0f);
    }
}
