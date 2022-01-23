using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenTime.Network.Player;

namespace BetweenTime.Player
{
    public class BTPlayerMovement : MonoBehaviour
    {
        [SerializeField] public BTPlayerInput Input;
        [SerializeField] public BTPlayerCameraMovement CameraMovement;
        public Camera Camera { get => CameraMovement.Camera; }
        [SerializeField] Transform playerTransform;
        [SerializeField] Transform faceOrientationTransform;
        readonly float sqrt2 = Mathf.Sqrt(2f);

        public void LookAt(Transform transform)
        {
            faceOrientationTransform.LookAt(transform);
            playerTransform.Rotate(0f, faceOrientationTransform.rotation.y - playerTransform.rotation.y, 0f);
            faceOrientationTransform.Rotate(-faceOrientationTransform.rotation.x, 0f, 0f);
        }

        void OnEnable() {
            Debug.Log("Enable movement");
            Input.EventAxis.AddListener(Move);
            Input.EventMouse.AddListener(Look);
        }

        void OnDisable() {
            Debug.Log("Disable movement");
            Input.EventAxis.RemoveListener(Move);
            Input.EventMouse.RemoveListener(Look);
        }

        void Move(float x, float y)
        {
            if (x != 0f && y != 0f)
            {
                x /= sqrt2;
                y /= sqrt2;
            }
            playerTransform.Translate((playerTransform.forward * y + playerTransform.right * x) * Time.deltaTime * 2f);
        }

        void Look(float x, float y)
        {
            playerTransform.Rotate(0f, x, 0f);
            faceOrientationTransform.Rotate(-y, 0f, 0f);
        }
    }
}
