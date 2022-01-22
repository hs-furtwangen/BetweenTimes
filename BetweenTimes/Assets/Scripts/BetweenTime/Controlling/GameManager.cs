using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DebugHelper;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace BetweenTime.Controlling
{
    public struct GameData
    {
        public bool isGhost;
    }

    public class GameManager : NetworkBehaviour
    {
        [Header("Lifecycle")] 
        [SyncVar] public float sessiontime;
        [SerializeField] private float totalTime = 3600f;
        
        [Header("Data")]
        private GameData _data;
        private bool _paused;
        
        [SerializeField] private List<GameObject> players = new List<GameObject>();

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
        public UnityEvent<GameData> OnGameDataSet = new UnityEvent<GameData>();
        public UnityEvent OnGameStart;
        public UnityEvent OnGameEnded;
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
            OnGameEnded?.Invoke();
            RpcOnEnd();
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
        public void RpcSetGameData(GameData data)
        {
           ClientSetGameData(data);
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
        #endregion Com Wrapper
        #endregion
        
        #region Client Logic
        [Client]
        public void ClientSetGameData(GameData data)
        {
            DebugColored.Log(showDebug, debugColor, "[Client]",this,"OnSetGameData");
            _data = data;
            OnGameDataSet?.Invoke(_data);
            
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

        #endregion Client Logic
        

    }
}