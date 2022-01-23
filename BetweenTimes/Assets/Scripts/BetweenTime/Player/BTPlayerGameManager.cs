using System;
using System.Collections;
using System.Collections.Generic;
using BetweenTime.Controlling;
using BetweenTime.Network.Player;
using DebugHelper;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class BTPlayerGameManager : NetworkBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private BTPlayerInput _playerInput;
    
    private bool _isReady;

    private bool _isRegsitered;
    
    public UnityEvent<bool> EventReadyStateChanged = new UnityEvent<bool>();
    public UnityEvent EventOnGameStart;
    public UnityEvent EventOnGameEnd;
    public UnityEvent EventOnGameReset;

    public void Start()
    {
        if (_gameManager == null)
            _gameManager = GameManager.Instance();

        if (_playerInput == null)
            _playerInput = GetComponent<BTPlayerInput>();

        if (!isLocalPlayer)
            return;
        
        AssignAtGameManager();
        Initialize();
    }

    private void Initialize()
    {
        if (!_isRegsitered)
        {
            _gameManager.OnGameStart.AddListener(OnGameStart);
            _gameManager.OnGameEnded.AddListener(OnGameEnd);
            _gameManager.OnGameReset.AddListener(OnGameReset);
            _isRegsitered = true;
        }

        _playerInput.EventOnFireUp.AddListener(ToggleReady);
    }
    
    public void ToggleReady()
    {
        _isReady = !_isReady;
        CmdSendReadyState(_isReady);

        EventReadyStateChanged?.Invoke(_isReady);
    }
    
    public void OnGameStart()
    {
        _playerInput.EventOnFireUp.RemoveListener(ToggleReady);
        
        EventOnGameStart?.Invoke();
    }

    public void OnGameEnd()
    {
        EventOnGameEnd?.Invoke();
    }

    public void OnGameReset()
    {
        EventOnGameReset?.Invoke();
        Initialize();
    }
    
    [Client]
    public void AssignAtGameManager()
    {
        CmdAssignAtGameManager();
    }

    [Command]
    public void CmdAssignAtGameManager()
    {
        if (_gameManager.AssignAtGameManager(gameObject))
        {
            RpcOnAssignAtManager();
        }
    }

    [ClientRpc]
    public void RpcOnAssignAtManager()
    {
        DebugColored.Log(true, Color.cyan, this, "Is registered!");
    }
    


    [Command]
    public void CmdSendReadyState(bool isReady)
    {
        _isReady = isReady;
        _gameManager.SetPlayerReady(gameObject, _isReady);
    }
    
}
