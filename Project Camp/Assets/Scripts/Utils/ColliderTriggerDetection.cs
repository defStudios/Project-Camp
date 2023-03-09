using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class ColliderTriggerDetection : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collider> onTriggerEnter;
        [SerializeField] private UnityEvent<Collider> onTriggerExited;

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            onTriggerExited?.Invoke(other);
        }
    }
}
