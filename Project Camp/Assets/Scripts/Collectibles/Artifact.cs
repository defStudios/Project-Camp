using System;
using GUI;
using Inventory.Items;
using UnityEngine;

namespace Collectibles
{
    public class Artifact : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField] private float interactionDuration = 1;
        [SerializeField] private ProgressableObject symbolProgress;
        
        private IItem _item;
        private Core.Player _player;

        private bool _available;

        private void Start()
        {
            _item = new ArtifactItem();
            _available = true;
        }

        public void TriggerEntered(Collider other)
        {
            if (!_available)
                return;
            
            bool isPlayer = other.TryGetComponent<Core.Player>(out var player);
            if (!isPlayer)
                return;

            _player = player;
            EnableInteraction();
        }

        public void TriggerExited(Collider other)
        {
            if (!_available)
                return;
            
            bool isPlayer = other.TryGetComponent<Core.Player>(out var player);
            if (!isPlayer)
                return;

            _player = player;
            DisableInteraction();
        }

        private void EnableInteraction()
        {
            _player.Input.EnableInteraction(origin, interactionDuration);
            
            _player.Input.OnInteractionCompleted += OnInteractionCompleted;
            _player.Input.OnInteractionProgressChanged += OnInteractionProgressChanged;
            _player.Input.OnInteractionInterrupted += OnInteractionInterrupted;
        }

        private void OnInteractionProgressChanged(float progress)
        {
            symbolProgress.SetProgress(progress);
        }

        private void OnInteractionInterrupted()
        {
            symbolProgress.SetProgress(0);            
        }

        private void OnInteractionCompleted()
        {
            _player.Orbit.EnableWisp();
            _player.Inventory.AddItem(_item);
                
            DisableInteraction();
            
            _available = false;
            symbolProgress.SetProgress(1);
        }
        
        private void DisableInteraction()
        {
            _player.Input.DisableInteraction();
            
            _player.Input.OnInteractionCompleted -= OnInteractionCompleted;
            _player.Input.OnInteractionProgressChanged -= OnInteractionProgressChanged;
            _player.Input.OnInteractionInterrupted -= OnInteractionInterrupted;
        }
    }
}
