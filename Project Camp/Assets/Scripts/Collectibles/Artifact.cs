using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectibles
{
    public class Artifact : MonoBehaviour
    {
        void Start()
        {

        }


        private void OnCollisionEnter(Collision collision)
        {
            bool isPlayer = collision.gameObject.TryGetComponent<Core.Player>(out var player);
            if (!isPlayer)
                return;
        
            player.CollectArtifact(this);
            DestroyArtifact();
        }

        private void DestroyArtifact()
        {
            gameObject.SetActive(false);
        }
    }
}
