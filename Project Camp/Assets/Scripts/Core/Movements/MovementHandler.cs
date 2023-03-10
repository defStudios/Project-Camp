using UnityEngine;

namespace Core.Movements
{
    public class MovementHandler : IMoveable
    {
        private Transform _transform;
        private Transform _groundChecker;
        private Transform _orientation;
        private Rigidbody _rigidbody;

        private float _moveSpeed;
        private float _jumpForce;
        private float _jumpAirMultiplier;

        private Vector3 _inputDirection;

        private bool _isOnGround;

        private float _groundDrag;
        private LayerMask _groundLayers;

        private const float _heightOffset = .2f;

        public MovementHandler(Transform transform, Transform orientation, Rigidbody rigidbody,
            Transform groundChecker, float groundDrag, LayerMask groundLayers,
            float moveSpeed, float jumpForce, float jumpAirMultiplier)
        {
            _transform = transform;
            _orientation = orientation;
            _rigidbody = rigidbody;
            _groundChecker = groundChecker;

            _groundDrag = groundDrag;
            _groundLayers = groundLayers;

            _moveSpeed = moveSpeed;
            _jumpForce = jumpForce;
            _jumpAirMultiplier = jumpAirMultiplier;
        }

        public void Enable()
        {
            _rigidbody.useGravity = true;
        }

        public void Disable()
        {
            _rigidbody.useGravity = true;
        }

        public void Tick(float deltaTime)
        {
            _isOnGround = IsOnGround();
            _rigidbody.drag = _isOnGround ? _groundDrag : 0;
        }

        public void FixedTick(float fixedDeltaTime)
        {
            float multi = _isOnGround ? 1 : _jumpAirMultiplier;

            var moveDirection = _orientation.forward * _inputDirection.z + _orientation.right * _inputDirection.x;
            _rigidbody.AddForce(moveDirection.normalized * _moveSpeed * multi, ForceMode.Force);

            LimitVelocity();
        }

        public bool IsOnGround()
        {
            return Physics.Raycast(_groundChecker.position, Vector3.down, _heightOffset, _groundLayers);
        }

        public void Move(Vector3 cameraPosition, Vector3 input)
        {
            _inputDirection = input;
        }

        public void Stop()
        {
            _inputDirection = Vector3.zero;
        }

        public void TryJump()
        {
            if (!_isOnGround)
                return;

            Jump();
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
