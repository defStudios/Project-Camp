using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;

        private Movements.MovementHandler _movement;
        private Input.InputController _inputs;

        private void Start()
        {
            _movement = new Movements.MovementHandler(transform, rb);

        }

        private void Update()
        {
            float delta = Time.deltaTime;
            
            _movement.Tick(delta);
        }
    }
}
