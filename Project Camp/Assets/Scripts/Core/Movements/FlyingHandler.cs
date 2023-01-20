using UnityEngine;

namespace Core.Movements
{
    public class FlightHandler : IMoveable
    {
        private Transform _orientation;
        private Rigidbody _rigidbody;

        private float _flightSpeed;
        private float _flightDrag;
        
        private Vector3 _inputDirection;

        public FlightHandler(Transform orientation, Rigidbody rigidbody,
            float fightSpeed, float flightDrag)
        {
            _orientation = orientation;
            _rigidbody = rigidbody;

            _flightSpeed = fightSpeed;
            _flightDrag = flightDrag;
        }

        public void Enable()
        {
            _rigidbody.useGravity = false;
            _rigidbody.drag = _flightDrag;
        }

        public void Tick(float deltaTime)
        {

        }

        public void FixedTick(float fixedDeltaTime)
        {
            var moveDirection = _orientation.forward * _inputDirection.z + _orientation.right * _inputDirection.x;
            moveDirection.y = _inputDirection.y;

            _rigidbody.AddForce(moveDirection.normalized * _flightSpeed, ForceMode.Force);

            LimitVelocity();
        }

        public void Move(Vector3 cameraPosition, Vector3 input)
        {
            _inputDirection = input;
        }

        private void LimitVelocity()
        {
            var flatVelocity = _rigidbody.velocity;
            if (flatVelocity.magnitude > _flightSpeed)
            {
                var limitedVelocity = flatVelocity.normalized * _flightSpeed;
                _rigidbody.velocity = limitedVelocity;
            }
        }
    }
}
