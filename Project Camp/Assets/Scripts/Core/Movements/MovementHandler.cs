using System.Collections.Generic;
using UnityEngine;

namespace Core.Movements
{
    public class MovementHandler
    {
        public enum MovementType
        {
            Walking,
            Flying
        }

        private MovementType _moveType;

        private Transform _transform;
        private Transform _orientation;
        private Rigidbody _rigidbody;

        private float _moveSpeed;
        private float _flySpeed;
        private float _jumpForce;
        private float _jumpAirMultiplier;

        private Vector3 _moveDirection;
        private Vector3 _inputDirection;

        private float _height;
        private float _groundDrag;
        private LayerMask _groundLayers;

        private bool _isOnGround;

        private const float _heightOffset = .2f;

        public MovementHandler(Transform transform, Transform orientation, Rigidbody rigidbody,
            float height, float groundDrag, LayerMask groundLayers, 
            float moveSpeed, float flySpeed, float jumpForce, float jumpAirMultiplier)
        {
            _transform = transform;
            _orientation = orientation;
            _rigidbody = rigidbody;

            _height = height;
            _groundDrag = groundDrag;
            _groundLayers = groundLayers;

            _moveType = MovementType.Walking;
            _moveSpeed = moveSpeed;
            _flySpeed = flySpeed;
            _jumpForce = jumpForce;
            _jumpAirMultiplier = jumpAirMultiplier;
        }

        public void Tick(float deltaTime)
        {
            _isOnGround = Physics.Raycast(_transform.position, Vector3.down, _height * .5f + _heightOffset, _groundLayers);
            _rigidbody.drag = _isOnGround ? _groundDrag : 0;
        }

        public void FixedTick(float fixedDeltaTime)
        {
            float multi = _isOnGround ? 1 : _jumpAirMultiplier;

            _moveDirection = _orientation.forward * _inputDirection.z + _orientation.right * _inputDirection.x;
            _rigidbody.AddForce(_moveDirection.normalized * _moveSpeed * multi, ForceMode.Force);

            LimitVelocity();
        }

        public void Move(Vector3 direction)
        {
            _inputDirection = direction;
        }

        public void TryJump()
        {
            if (!_isOnGround)
                return;

            Jump();
        }

        public void ToggleFlyingMode()
        {
            _moveType = _moveType == MovementType.Flying ? MovementType.Walking : MovementType.Flying;
        }

        private void Jump()
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(_transform.up * _jumpForce, ForceMode.Impulse);
        }

        private void LimitVelocity()
        {
            var flatVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            if (flatVelocity.magnitude > _moveSpeed)
            {
                var limitedVelocity = flatVelocity.normalized * _moveSpeed;
                limitedVelocity.y = _rigidbody.velocity.y;

                _rigidbody.velocity = limitedVelocity;
            }
        }
    }
}
