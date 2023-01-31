using Inventory.Items;
using UnityEngine;

namespace Collectibles
{
    public class Artifact : MonoBehaviour
    {
        private IItem item;

        private void Start()
        {
            item = new ArtifactItem();
        }

        private void OnCollisionEnter(Collision collision)
        {
            bool isPlayer = collision.gameObject.TryGetComponent<Core.Player>(out var player);
            if (!isPlayer)
                return;
        
            player.Inventory.AddItem(item);
            DestroyArtifact();
        }

        private void DestroyArtifact()
        {
            gameObject.SetActive(false);
        }
    }
}
