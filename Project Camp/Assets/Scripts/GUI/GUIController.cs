using System;
using Core.Services;
using UnityEngine;

namespace GUI
{
    public class GUIController : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRect;
        
        [Space, SerializeField] private ProgressableObject progressbar;
        [SerializeField] private MessageQueue message;

        [Space, SerializeField] private RectTransform interactionPanel;
        [SerializeField] private ProgressableObject interactionProgress;
        [SerializeField] private Vector3 interactionPanelOffset;
        
        private Camera _camera;
        private Core.Player _player;
        private Environment.LevelEnvironment _level;

        private Transform _interactionOrigin;

        public void Init(Camera camera, Core.Player player, Environment.LevelEnvironment level)
        {
            _camera = camera;
            _player = player;
            _level = level;

            progressbar.SetProgress(0, true);
            
            ServiceManager.Container.Register<IMessageHandler>(message);

            player.Inventory.InventoryStateChanged += UpdatePlayerProgress;

            player.Input.OnInteractionStateChanged += ChangeInteractionState;
            player.Input.OnInteractionProgressChanged += UpdateInteractionProgress;
            player.Input.OnInteractionCompleted += OnInteractionCompleted;
            player.Input.OnInteractionInterrupted += OnInteractionInterrupted;
        }

        private void Update()
        {
            if (interactionPanel.gameObject.activeInHierarchy)
            {
                var canvasSize = canvasRect.rect.size;
                var relativePos = GetRelativePosOfWorldPoint(_interactionOrigin.position, _camera);
                interactionPanel.anchorMin = interactionPanel.anchorMax = Vector2.zero; // set bottom left anchor
                interactionPanel.anchoredPosition = relativePos * canvasSize;
                
                interactionPanel.transform.localPosition += interactionPanelOffset;
            }
        }

        private void UpdatePlayerProgress()
        {
            progressbar.SetProgress(_player.Inventory.GetArtifactsCount() / (float)_level.ArtifactsTotalCount);
        }

        private void ChangeInteractionState(Transform interactionOrigin, bool state)
        {
            _interactionOrigin = interactionOrigin;
            interactionProgress.SetProgress(0, true);
            
            interactionPanel.gameObject.SetActive(state);
        }

        private void UpdateInteractionProgress(float progress)
        {
            interactionProgress.SetProgress(progress);
        }
        
        private void OnInteractionInterrupted()
        {
            interactionProgress.SetProgress(0);
        }
        
        private void OnInteractionCompleted()
        {
            ChangeInteractionState(null, false);
        }
        
         // Returns a normalized [0,1] position counted from camera bottom left
        public static Vector2 GetRelativePosOfWorldPoint(Vector3 worldPoint, Camera camera) {
            Vector3 screenPoint = camera.WorldToScreenPoint(worldPoint);
            return new Vector2(screenPoint.x / camera.pixelWidth, screenPoint.y / camera.pixelHeight);
        }
    }
}
