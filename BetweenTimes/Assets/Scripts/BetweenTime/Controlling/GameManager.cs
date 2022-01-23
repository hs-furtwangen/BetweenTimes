using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BetweenTime.Network.Player;
using DebugHelper;
using Mirror;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using Random = System.Random;

namespace BetweenTime.Controlling
{
    public struct GamePlayerData
    {
        public bool isGhost;
        public bool isReady;

        public GamePlayerData(bool isGhost, bool isReady)
        {
            this.isGhost = isGhost;
            this.isReady = isReady;
        }
    }

    public class GameManager : NetworkBehaviour
    {
        [Header("Player")]
        [SerializeField] private int playerAmountToStart = 2;
        [SerializeField] private Dictionary<GameObject, GamePlayerData> playerIsReady = new Dictionary<GameObject, GamePlayerData>();
        private bool allConnected;
        [SerializeField] private Transform spawnPositionGhost;
        [SerializeField] private Transform spawnPositionHuman;
        
        [Header("Lifecycle")] 
        [SyncVar] public float sessiontime;
        [SerializeField] private float totalTime = 3600f;
        
        [Header("Data")]
        private GamePlayerData _playerData;
        private bool _paused;


        [Header("Flags")] private bool _started;

        public bool Started => _started;

        [Header("Debug")] 
        [SerializeField] private bool showDebug;
        [SerializeField] private Color debugColor;
        
        #region Singleton

        private static GameManager instance;

        public static GameManager Instance()
        {
            return instance;
        }
        
        #endregion Singleton

        #region Events
        public UnityEvent<GamePlayerData> OnGameDataSet = new UnityEvent<GamePlayerData>();
        public UnityEvent OnGameStart;
        public UnityEvent OnGameEnded;
        public UnityEvent OnGameReset;
        #endregion

        private Coroutine c_lifeTime;
        
        #region Standard Unity

        public void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        #endregion Standard Unity
        
        #region Lifecycle

        [ContextMenu("StartLifeCycle")]
        [Server]
        public void StartLifecycle()
        {
            if (!isServer)
                return;
            
            DebugColored.Log(showDebug, debugColor, "[Server]",this,"Start Lifecycle");
            sessiontime = totalTime;
            
            if(c_lifeTime != null)
                StopCoroutine(c_lifeTime);

            _started = true;
            OnGameStart?.Invoke();
            RpcOnStart();
            c_lifeTime = StartCoroutine(LifetimeCoroutine());
        }
        
        [ContextMenu("End Lifecycle")]
        [Server]
        public void EndLifecycle()
        {
            DebugColored.Log(showDebug, debugColor, "[Server]",this,"End Lifecycle");
            _started = false;
            
            if(c_lifeTime!= null)
                StopCoroutine(c_lifeTime);
            
            OnGameEnded?.Invoke();
            RpcOnEnd();
        }

        [ContextMenu("Reset Lifecycle")]
        [Server]
        public void ResetLifecycle()
        {
            if(c_lifeTime != null)
                StopCoroutine(c_lifeTime);
            
            DebugColored.Log(showDebug, debugColor, "[Server]",this,"Reset Lifecycle");
            sessiontime = totalTime;
            
            _started = false;

            ResetPlayerDictionary();

            ResetPosition();
            
            RpcOnReset();

            OnGameReset?.Invoke();
        }

        [Server]
        private void ResetPosition()
        {
            DebugColored.Log(showDebug, debugColor, "[Server]",this,"Reset Positions");
            foreach (var player in playerIsReady)
            {
                NetworkConnection con = player.Key.GetComponent<NetworkIdentity>().connectionToClient;
                if (player.Value.isGhost)
                {
                    DebugColored.Log(showDebug, debugColor, "[Server]",this,"Player is Ghost, set to "+spawnPositionGhost.position);
                    TargetSetPlayerSpawnPosition(con, player.Key, spawnPositionGhost.position, spawnPositionGhost.rotation);
                }
                else
                {
                    DebugColored.Log(showDebug, debugColor, "[Server]",this,"Player is Human, set to "+spawnPositionHuman.position);
                    TargetSetPlayerSpawnPosition(con, player.Key, spawnPositionHuman.position, spawnPositionHuman.rotation);
                }
            }
        }

        [TargetRpc]
        public void TargetSetPlayerSpawnPosition(NetworkConnection con, GameObject player, Vector3 position, Quaternion rotation)
        {
            DebugColored.Log(showDebug, debugColor, "[ClientTarget]",this,"SetPosition "+position);
            player.transform.position = position;
            player.transform.rotation = rotation;
        }

        private void ResetPlayerDictionary()
        {
            DebugColored.Log(showDebug, debugColor, "[Server]",this,"Reset Player States");
            List<GameObject> players = new List<GameObject>();

            foreach (var player in playerIsReady)
            {
                players.Add(player.Key);
            }

            for (int i = 0; i < players.Count; i++)
            {
                playerIsReady[players[i]] = new GamePlayerData(!playerIsReady[players[i]].isGhost,false);
            }
        }

        IEnumerator LifetimeCoroutine()
        {
            while (sessiontime > 0)
            {
                if(!_paused)
                    sessiontime -= Time.deltaTime;
                
                yield return null;
            }
            
            EndLifecycle();
        }

        #region Com Wrapper
        [ClientRpc]
        public void RpcSetGameData(GamePlayerData playerData)
        {
           ClientSetGameData(playerData);
        }
        
        [ClientRpc]
        public void RpcOnStart()
        {
            ClientOnStart();
        }
        
        [ClientRpc]
        public void RpcOnEnd()
        {
            ClientOnEnd();
        }
        
        [ClientRpc]
        public void RpcOnReset()
        {
            ClientOnReset();
        }
        #endregion Com Wrapper
        #endregion
        
        #region Client Logic
        [Client]
        public void ClientSetGameData(GamePlayerData playerData)
        {
            DebugColored.Log(showDebug, debugColor, "[Client]",this,"OnSetGameData");
            _playerData = playerData;
            OnGameDataSet?.Invoke(_playerData);
            
        }
        
        [Client]
        public void ClientOnStart()
        {
            DebugColored.Log(showDebug, debugColor, "[Client]",this,"OnGameStart");
            _started = true;
            OnGameStart?.Invoke();
        }
        
        [Client]
        public void ClientOnEnd()
        {
            DebugColored.Log(showDebug, debugColor, "[Client]",this,"OnGameEnd");
            _started = false;
            OnGameEnded?.Invoke();
        }
        
        [Client]
        public void ClientOnReset()
        {
            DebugColored.Log(showDebug, debugColor, "[Client]",this,"OnReset");
            _started = false;
            OnGameReset?.Invoke();
        }
        #endregion Client Logic
        
        
        #region GameCommunication

        [Server]
        public bool AssignAtGameManager(GameObject netObjectPlayer)
        {
            if (playerIsReady.ContainsKey(netObjectPlayer))
            {
                DebugColored.Log(showDebug,debugColor,this,"Player "+netObjectPlayer+" already regsitered.");
                return false;
            }

            GamePlayerData playerData;
            if (playerIsReady.Count == 0)
            {
                playerData = new GamePlayerData(true, false);
            }else
                playerData = new GamePlayerData(false, false);

            playerIsReady.Add(netObjectPlayer,playerData);

            NetworkConnection con = netObjectPlayer.GetComponent<NetworkIdentity>().connectionToClient;
            if(playerIsReady[netObjectPlayer].isGhost)
            TargetSetPlayerSpawnPosition(con, netObjectPlayer, spawnPositionGhost.position, 
                spawnPositionGhost.rotation);
            else
                TargetSetPlayerSpawnPosition(con, netObjectPlayer, spawnPositionHuman.position, 
                    spawnPositionHuman.rotation);

            if (playerIsReady.Count == playerAmountToStart)
                allConnected = true;
            
            return true;
        }

        [Server]
        public void SetPlayerReady(GameObject playerObj, bool isReady)
        {
            if (playerIsReady.ContainsKey(playerObj))
                playerIsReady[playerObj] = new GamePlayerData(playerIsReady[playerObj].isGhost,isReady);

            if (allConnected)
                if(CheckIfAllReady())
                    StartLifecycle();
        }

        private bool CheckIfAllReady()
        {
            foreach (var player in playerIsReady)
            {
                if (!player.Value.isReady)
                    return false;
            }

            return true;
        }
        #endregion
    }

}