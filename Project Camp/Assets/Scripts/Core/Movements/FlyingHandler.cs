using UnityEngine;

namespace Core.Movements
{
    public class FlightHandler 
    {
        public bool ModeActive { get; private set; }

        private Transform _transform;
        private Transform _orientation;
        private Rigidbody _rigidbody;

        private float _flightSpeed;
        private float _flightDrag;
        
        private Vector3 _inputDirection;
        private Vector3 _camDirection;

        private bool _isOnGround;
        private LayerMask _groundLayers;

        private float _height;
        private const float _heightOffset = .2f;

        public FlightHandler(Transform transform, Transform orientation, Rigidbody rigidbody,
            float fightSpeed, float flightDrag, float height, LayerMask groundLayers)
        {
            _transform = transform;
            _orientation = orientation;
            _rigidbody = rigidbody;

            _flightSpeed = fightSpeed;
            _flightDrag = flightDrag;

            _height = height;
            _groundLayers = groundLayers;
        }

        public void Enable()
        {
            ModeActive = true;

            _rigidbody.useGravity = false;
            _rigidbody.drag = _flightDrag;
        }

        public void Disable()
        {
            ModeActive = false;

        }

        public void Tick(float deltaTime)
        {
            _isOnGround = Physics.Raycast(_transform.position, Vector3.down, _height * .5f + _heightOffset, _groundLayers);

            if (_isOnGround)
                ; // Disable flying mode & enable movement
        }

        public void FixedTick(float fixedDeltaTime)
        {
            if (!ModeActive)
                return;


            var moveDirection = new Vector3(
                _camDirection.x * _inputDirection.z,
                _camDirection.y * _inputDirection.z,
                _camDirection.z * _inputDirection.z);


            //_orientation.forward * _inputDirection.z + 
            //_orientation.up * _inputDirection.z + 
            //_orientation.right * _inputDirection.x;

            _rigidbody.AddForce(moveDirection.normalized * _flightSpeed, ForceMode.Force);



            //var moveDirection = _inputDirection;    //_orientation.forward * _inputDirection.z + _orientation.right * _inputDirection.x;
            //_rigidbody.AddForce(moveDirection.normalized * _flightSpeed, ForceMode.Force);

            LimitVelocity();
        }

        public void Move(Vector3 cameraPosition, Vector3 input)
        {
            _inputDirection = input;
            _camDirection = (_transform.position - cameraPosition).normalized;
        }

        public void ChangeAltitude(Vector3 direction)
        {
            
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
