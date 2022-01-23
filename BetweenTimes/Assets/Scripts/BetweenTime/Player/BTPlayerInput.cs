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
        [SerializeField] private bool _isDisabled;
        
        #region Events
        public UnityEvent EventFireDown;
        public UnityEvent EventFire;
        public UnityEvent EventFireUp;
        public UnityEvent EventRightDown;
        public UnityEvent EventRight;
        public UnityEvent EventRightUp;
        public UnityEvent<float, float> EventAxis = new UnityEvent<float, float>();
        public UnityEvent<float, float> EventMouse = new UnityEvent<float, float>();
        #endregion
        
        public void EnableInput()
        {
            _isDisabled = true;
        }

        public void DisableInput()
        {
            _isDisabled = false;
        }
        
        public void Update()
        {
            if (!_isDisabled)
                return;
            
            if (Input.GetButtonDown("Fire1"))
            {
                EventFireDown?.Invoke();
            }
            if (Input.GetButton("Fire1"))
            {
                EventFire?.Invoke();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                EventFireUp?.Invoke();
            }
            if (Input.GetButtonDown("Fire2"))
            {
                EventRightDown?.Invoke();
            }
            if (Input.GetButton("Fire2"))
            {
                EventRight?.Invoke();
            }
            if (Input.GetButtonUp("Fire2"))
            {
                EventRightUp?.Invoke();
            }
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                EventAxis.Invoke(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                EventMouse.Invoke(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }
        }
    }
}