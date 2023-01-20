using UnityEngine;

namespace Core.Movements
{
    public class MovementHandler
    {
        private Transform _transform;
        private Transform _orientation;
        private Rigidbody _rigidbody;

        private float _moveSpeed;
        private float _jumpForce;
        private float _jumpAirMultiplier;

        private bool _modeActive;

        private Vector3 _inputDirection;

        private float _height;
        private float _groundDrag;
        private LayerMask _groundLayers;

        private bool _isOnGround;

        private const float _heightOffset = .2f;

        public MovementHandler(Transform transform, Transform orientation, Rigidbody rigidbody,
            float height, float groundDrag, LayerMask groundLayers,
            float moveSpeed, float jumpForce, float jumpAirMultiplier)
        {
            _transform = transform;
            _orientation = orientation;
            _rigidbody = rigidbody;

            _height = height;
            _groundDrag = groundDrag;
            _groundLayers = groundLayers;

            _moveSpeed = moveSpeed;
            _jumpForce = jumpForce;
            _jumpAirMultiplier = jumpAirMultiplier;
        }

        public void Enable()
        {
            _modeActive = true;
            _rigidbody.useGravity = true;
        }

        public void Disable() => _modeActive = false;

        public void Tick(float deltaTime)
        {
            if (!_modeActive)
                return;

            _isOnGround = Physics.Raycast(_transform.position, Vector3.down, _height * .5f + _heightOffset, _groundLayers);
            _rigidbody.drag = _isOnGround ? _groundDrag : 0;
        }

        public void FixedTick(float fixedDeltaTime)
        {
            if (!_modeActive)
                return;

            float multi = _isOnGround ? 1 : _jumpAirMultiplier;

            var moveDirection = _orientation.forward * _inputDirection.z + _orientation.right * _inputDirection.x;
            _rigidbody.AddForce(moveDirection.normalized * _moveSpeed * multi, ForceMode.Force);

            LimitVelocity();
        }

        public void Move(Vector3 direction)
        {
            if (!_modeActive)
                return;

            _inputDirection = direction;
        }

        public void TryJump()
        {
            if (!_modeActive || !_isOnGround)
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
