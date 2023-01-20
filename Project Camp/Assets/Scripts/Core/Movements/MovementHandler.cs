using System.Collections.Generic;
using UnityEngine;

namespace Core.Movements
{
    public class MovementHandler
    {
        public enum MovementType
        {
            Running,
            Flying
        }

        private MovementType moveType;

        private Transform transform;
        private Rigidbody rb;

        private float moveSpeed;

        public MovementHandler(Transform transform, Rigidbody rb, float moveSpeed)
        {
            this.transform = transform;
            this.rb = rb;
            this.moveSpeed = moveSpeed;
        }

        public void Tick(float deltaTime)
        {

        }

        public void Move(Vector3 direction)
        {
            // 
        }

        public void Jump()
        {

        }

        public void ToggleFlyingMode()
        {

        }
    }
}
