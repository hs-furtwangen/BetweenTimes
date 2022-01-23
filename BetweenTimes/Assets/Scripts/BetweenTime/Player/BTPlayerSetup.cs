using System.Collections.Generic;
using BetweenTime.Player;
using Cinemachine;
using Mirror;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

namespace BetweenTime.Network.Player
{
    public class BTPlayerSetup : NetworkBehaviour
    {
        [Header("References")] 
        [SerializeField] private Camera camera;
        [SerializeField] private List<MonoBehaviour> playerScriptsToDelete = new List<MonoBehaviour>();
        private BTPlayerMovement _btPlayerMovement;
        
        [Header("Flags")] private bool _finishedSetup;
        public bool FinishedSetup => _finishedSetup;
        
        #region Events
        [Tooltip("Triggers when the GameObject is a LocalPlayer and has finished its setup.")]
        public UnityEvent OnFinishedSetupLocalPlayer;
        
        [Tooltip("Triggers when the GameObject isnt a LocalPlayer and has finished its setup-")]
        public UnityEvent OnFinishedSetupNonLocalPlayer;
        #endregion Events

        private void Start()
        {
            if (!isLocalPlayer)
            {
            
                if (playerScriptsToDelete.Count > 0)
                    for (int i = playerScriptsToDelete.Count - 1; i >= 0; i--)
                    {
                        Destroy(playerScriptsToDelete[i]);
                    }

                camera.enabled = false;
                OnFinishedSetupNonLocalPlayer?.Invoke();
                return;
            }

            Rigidbody rb = GetComponent<Rigidbody>();
            if(rb != null) Destroy(rb);
            
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            if(collider != null) Destroy(collider);
            
            if(_btPlayerMovement == null) _btPlayerMovement = gameObject.AddComponent<BTPlayerMovement>();
            _btPlayerMovement.enabled = false;
            _finishedSetup = true;
            OnFinishedSetupLocalPlayer?.Invoke();
        }
        
    }
}