using System.Collections.Generic;
using UnityEngine;

namespace Core.Collisions
{
    [RequireComponent(typeof(Collider))]
    public class CollisionHandler : MonoBehaviour
    {
        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            // mb this logic must be on the collision target side
        }
    }
}
