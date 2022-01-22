using System;
using BetweenTime.Controlling;
using DebugHelper;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace BetweenTime.Network.Player
{
    /// <summary>
    /// Main Controller for the Player.
    /// </summary>
    public class BTPlayerInput : MonoBehaviour
    {
        [SerializeField] private bool _started;
        
        #region Events
        public UnityEvent EventOnFireDown;
        public UnityEvent EventOnFire;
        public UnityEvent EventOnFireUp;
        public UnityEvent<float, float> EventOnAxis = new UnityEvent<float, float>();
        public UnityEvent<float, float> EventOnMouse = new UnityEvent<float, float>();
        #endregion

        public void StartInputObservation()
        {
            _started = true;
        }

        public void Update()
        {
            if (!_started)
                return;
            
            if (Input.GetButtonDown("Fire1"))
            {
                EventOnFireDown?.Invoke();
            }
            if (Input.GetButton("Fire1"))
            {
                EventOnFire?.Invoke();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                EventOnFireUp?.Invoke();
            }
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                EventOnAxis.Invoke(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                EventOnMouse.Invoke(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }
        }
    }
}