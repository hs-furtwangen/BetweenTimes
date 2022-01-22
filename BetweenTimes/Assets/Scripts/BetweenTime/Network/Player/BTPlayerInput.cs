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
        #endregion

        public void StartInputObservation()
        {
            _started = true;
        }

        public void Update()
        {
            if (!_started)
                return;
            
            // if (Input.GetButtonDown("Fire1"))
            // {
            //     EventOnFireDown?.Invoke();
            // }
            // if (Input.GetButton("Fire1"))
            // {
            //     EventOnFire?.Invoke();
            // }
            // if (Input.GetButtonUp("Fire1"))
            // {
            //     EventOnFireUp?.Invoke();
            // }
        }
    }
}