using System;
using Inventory.Items;
using UnityEngine;

namespace Collectibles
{
    public class Artifact : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField] private float interactionDuration = 1;
        
        private IItem _item;
        private Core.Player _player;

        private bool _available;
        private bool _interactable;

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
            _interactable = true;
            _player.Input.EnableInteraction(origin, interactionDuration);
            
            _player.Input.OnInteractionCompleted += OnInteractionCompleted;
        }

        private void OnInteractionCompleted()
        {
            _player.Inventory.AddItem(_item);
                
            DisableInteraction();
            DestroyArtifact();
        }
        
        private void DisableInteraction()
        {
            _interactable = false;
            
            _player.Input.DisableInteraction();
            _player.Input.OnInteractionCompleted -= OnInteractionCompleted;
        }
        
        private void DestroyArtifact()
        {
            gameObject.SetActive(false);
        }
    }
}
