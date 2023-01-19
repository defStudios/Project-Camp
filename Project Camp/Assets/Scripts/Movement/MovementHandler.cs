using System.Collections.Generic;
using UnityEngine;

namespace Core.Movements
{
    public class MovementHandler
    {
        private Transform transform;
        private Rigidbody rb;

        private float moveSpeed;

        public MovementHandler(Transform transform, Rigidbody rb, float moveSpeed)
        {
            this.transform = transform;
            this.rb = rb;
            this.moveSpeed = moveSpeed;
        }

        public void Move(Vector3 direction)
        {

        }

        public void Tick(float deltaTime)
        {

        }
    }
}
