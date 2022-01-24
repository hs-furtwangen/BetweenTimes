using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayerCameraMovement : MonoBehaviour
{
    [SerializeField] public Camera Camera;
    [SerializeField] Transform cameraOrientationTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform headTransform;
    [SerializeField] float followSpeed = 10f;
    public float FocalLength { get {
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit)) return hit.distance;
        return 10f;
    }}

    void LateUpdate()
    {
        cameraOrientationTransform.position = FollowPosition(cameraOrientationTransform.position, playerTransform.position);
        cameraOrientationTransform.rotation = FollowRotation(cameraOrientationTransform.rotation, playerTransform.rotation);
        Camera.transform.rotation = FollowRotation(Camera.transform.rotation, headTransform.rotation);
    }

    Vector3 FollowPosition(Vector3 current, Vector3 target)
    {
        float t = /*(10 / (10 + Vector3.Distance(current, target))) * */followSpeed * Time.deltaTime;
        return Vector3.Slerp(current, target, t);
    }

    Quaternion FollowRotation(Quaternion current, Quaternion target)
    {
        float t = /*(10 / (10 + Quaternion.Angle(current, target))) * */followSpeed * Time.deltaTime;
        return Quaternion.Slerp(current, target, t);
    }
}
