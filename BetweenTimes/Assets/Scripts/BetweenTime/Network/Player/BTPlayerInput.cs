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
        [SerializeField] private EventSystem _eventSystem;
        private bool _started;
        
        #region Events
        public UnityEvent EventOnFireDown;
        public UnityEvent EventOnFire;
        public UnityEvent EventOnFireUp;
        public UnityEvent<float> EventOnHorizontal = new UnityEvent<float>();
        public UnityEvent<float> EventOnVertical = new UnityEvent<float>();
        public UnityEvent<float> EventOnMouseX = new UnityEvent<float>();
        public UnityEvent<float> EventOnMouseY = new UnityEvent<float>();
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
            if (Input.GetAxis("Horizontal") != 0)
            {
                EventOnHorizontal.Invoke(Input.GetAxis("Horizontal"));
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                EventOnVertical.Invoke(Input.GetAxis("Vertical"));
            }
            if (Input.GetAxis("Mouse X") != 0)
            {
                EventOnHorizontal.Invoke(Input.GetAxis("Mouse X"));
            }
            if (Input.GetAxis("Mouse Y") != 0)
            {
                EventOnVertical.Invoke(Input.GetAxis("Mouse Y"));
            }
        }
    }
}