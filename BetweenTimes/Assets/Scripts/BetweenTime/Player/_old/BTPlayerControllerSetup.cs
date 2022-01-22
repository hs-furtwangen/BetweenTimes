using System.Collections.Generic;
using Cinemachine;
using Mirror;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

namespace BetweenTime.Network.Player
{
    public class BTPlayerControllerSetup : NetworkBehaviour
    {
        [Header("Prefabs")] [SerializeField] private GameObject cameraPrefab;
        [SerializeField] private GameObject virtualCameraPrefab;
        [SerializeField] private GameObject ui_EventSystemPrefab;
        [SerializeField] private GameObject uiPrefab;

        [Header("References")] 
        [SerializeField] private GameObject playerCapsule;
        [SerializeField] private GameObject playerCameraRoot;
        private Camera camera;
        private CinemachineVirtualCamera virtualCamera;
        private GameObject ui_EventSystem;
        private GameObject ui;
        [SerializeField] private List<MonoBehaviour> playerScriptsToDelete = new List<MonoBehaviour>();
        [SerializeField] private CharacterController _characterController;

        [Header("Flags")] private bool _finishedSetup;

        public bool FinishedSetup => _finishedSetup;

        #region Events
        public UnityEvent OnFinishedSetup;
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
                Destroy(_characterController);
                return;
            }
            playerCapsule.gameObject.tag="Player"; 
            ui_EventSystem = Instantiate(ui_EventSystemPrefab, transform);
            ui = Instantiate(uiPrefab, transform);
            if (Camera.main != null) Destroy(Camera.main.gameObject);
            camera = Instantiate(cameraPrefab, transform).GetComponent<Camera>();

            virtualCamera = Instantiate(virtualCameraPrefab, transform).GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = playerCameraRoot.transform;
            ui.GetComponent<UICanvasControllerInput>().starterAssetsInputs = playerCapsule.GetComponent<StarterAssetsInputs>();
            _finishedSetup = true;
        }
    }
}