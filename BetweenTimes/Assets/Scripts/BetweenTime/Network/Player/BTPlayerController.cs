using System;
using BetweenTime.Controlling;
using DebugHelper;
using Mirror;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BetweenTime.Network.Player
{
    public class BTPlayerData
    {
        public bool isGhost;
    }
    
    /// <summary>
    /// Main Controller for the Player.
    /// </summary>
    public class BTPlayerController : NetworkBehaviour
    {
        #region Signleton
        private static BTPlayerController instance;

        public static BTPlayerController GetLocalPlayer()
        {
            return instance;
        }

        #endregion
        
        [Header("Player")] private BTPlayerData data;

        [Header("References")] 
        private GameManager _manager;
        private BTPlayerSetup _setupModule;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private FirstPersonController _firstPersonController;
        [SerializeField] private PlayerInput _playerInput;

        public BTPlayerInput BtPlayerInput => _btPlayerInput;

        [SerializeField] private BTPlayerInput _btPlayerInput;

        [Header("Debug")] [SerializeField] private bool showDebug;
        [SerializeField] private Color debugColor;
        
        [Header("Flags")]
        [Tooltip("Flag for GameManager finished setup and started the Session")]
        private bool _gameManagerReady;

        private void Awake()
        {
            if (!isLocalPlayer)
                return;

            if (_btPlayerInput == null) _btPlayerInput.GetComponent<BTPlayerInput>(); 
            
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            //ref GameManager
            if (_manager == null)
            {
                _manager = GameManager.Instance();
            }

            //dont do anything if not local player
            if (!isLocalPlayer)
                return;
            
            //movement should only work, if the game has started
            DisableMovement();

            //set state of GameManager --> has already started --> start player
            if (_manager == null)
            {
                DebugColored.Log(showDebug, debugColor,this, "No GameManager!");
                _gameManagerReady = true;
            }
            else
            {
                if (_manager.Started)
                {
                    OnGameStart();
                }
                else
                {
                    _manager.OnGameStart.AddListener(OnGameStart);
                    _manager.OnGameEnded.AddListener(OnGameEnd);
                }
            }
            
            //manage player setup using the setupModule
            if (_setupModule == null)
                _setupModule = GetComponent<BTPlayerSetup>();

            if (_setupModule.FinishedSetup)
            {
                OnSetupModuleFinished();
            }
            else
                _setupModule.OnFinishedSetup.AddListener(OnSetupModuleFinished);
        }


        /// <summary>
        /// Gets called after the <see cref="BTPlayerSetup"/>-Module finished its setup.
        /// It should finish this script setup and then start the player.
        /// </summary>
        public void OnSetupModuleFinished()
        {
            _setupModule.OnFinishedSetup.RemoveListener(OnSetupModuleFinished);
            
            //check roll
            
            //if the session already started --> start player
            if(_gameManagerReady)
                StartPlayer();
        }

        /// <summary>
        /// Is started after the <see cref="BTPlayerSetup"/>-Module and the <see cref="GameManager"/> finished their setup.
        /// </summary>
        public void StartPlayer()
        {
            //do stuff like enable controls or blend in

            EnableMovement();

            if (_btPlayerInput == null) _btPlayerInput = GetComponent<BTPlayerInput>();
            if (_btPlayerInput != null) _btPlayerInput.StartInputObservation();
        }

        #region Movement
        private void EnableMovement()
        {
            _characterController.enabled = true;
            _firstPersonController.enabled = true;
            _playerInput.enabled = true;
        }
        private void DisableMovement()
        {
            _characterController.enabled = false;
            _firstPersonController.enabled = false;
            _playerInput.enabled = false;
        }
        #endregion Movement
        
        #region Manager Callbacks
        public void OnGameStart()
        {
            _gameManagerReady = true;
            
            if(_setupModule.FinishedSetup)
                StartPlayer();
        }

        public void OnGameEnd()
        {
            
        }
        #endregion Manager Callbacks
    }
}