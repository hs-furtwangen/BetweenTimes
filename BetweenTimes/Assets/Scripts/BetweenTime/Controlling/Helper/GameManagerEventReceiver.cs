using System;
using UnityEngine;
using UnityEngine.Events;

namespace BetweenTime.Controlling.Helper
{
    public class GameManagerEventReceiver : MonoBehaviour
    {
        private GameManager manager;
        private bool _isRegistered;

        [SerializeField] private bool onDataSet = true;
        [SerializeField] private bool onStart = true;
        [SerializeField] private bool onEnded = true;

        #region Events

        public UnityEvent<GamePlayerData> EventOnGameDataSet = new UnityEvent<GamePlayerData>();
        public UnityEvent EventOnGameStart;
        public UnityEvent EventOnGameEnded;

        #endregion Events
        
        #region Standard Unity
        private void OnEnable()
        {
            if(manager == null) manager = GameManager.Instance();
            if (manager != null)
            {
                RegisterAtManager();
            }
        }

        private void OnDisable()
        {
            if(manager == null) manager = GameManager.Instance();
            if (manager != null)
            {
                DeregisterAtManager();
            }
        }

        private void Awake()
        {
            if(manager == null) manager = GameManager.Instance();
            if (manager != null)
            {
                RegisterAtManager();
            }
        }
        #endregion Standard Unity
        
        #region Registration
        private void RegisterAtManager()
        {
            if (_isRegistered)
                return;

            if(onDataSet) manager.OnGameDataSet.AddListener(OnGameDataSet);
            if(onStart) manager.OnGameStart.AddListener(OnGameStart);
            if(onEnded) manager.OnGameEnded.AddListener(OnGameEnded);

            _isRegistered = true;
        }

        private void DeregisterAtManager()
        {
            if (!_isRegistered)
                return;

            if(onDataSet) manager.OnGameDataSet.RemoveListener(OnGameDataSet);
            if(onStart) manager.OnGameStart.RemoveListener(OnGameStart);
            if(onEnded) manager.OnGameEnded.RemoveListener(OnGameEnded);

            _isRegistered = false;
        }
        #endregion Registration
        
        public virtual void OnGameDataSet(GamePlayerData playerData)
        {
            EventOnGameDataSet?.Invoke(playerData);
        }
        
        public virtual void OnGameStart()
        {
            EventOnGameStart?.Invoke();
        }
        public virtual void OnGameEnded()
        {
            EventOnGameEnded?.Invoke();
        }
    }
}
