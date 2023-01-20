using UnityEngine;

namespace Core.Movements
{
    public class RotationHandler
    {
        private Transform _cameraTransform;

        private Transform _transform;
        private Transform _model;
        private Transform _orientation;

        private float _deltaTime;
        private float _rotationSpeed;

        private Vector3 _orientationForward;
        private Vector3 _inputDirection;

        public RotationHandler(Transform transform, Transform model, Transform orientation, Transform cameraTransform, float rotationSpeed)
        {
            _transform = transform;
            _model = model;
            _orientation = orientation;
            _cameraTransform = cameraTransform;

            _rotationSpeed = rotationSpeed;
        }

        public void Tick(float deltaTime)
        {
            _deltaTime = deltaTime;

            if (_orientationForward != Vector3.zero)
                _orientation.forward = _orientationForward;

            if (_inputDirection != Vector3.zero)
                _model.forward = Vector3.Slerp(_model.forward, _inputDirection.normalized, _deltaTime * _rotationSpeed);
        }

        public void FixedTick(float deltaTime)
        {

        }

        public void Rotate(Vector3 direction)
        {
            var camProjectedPos = new Vector3(_cameraTransform.position.x, _transform.position.y, _cameraTransform.position.z);
            _orientationForward = (_transform.position - camProjectedPos).normalized;

            _inputDirection = _orientation.forward * direction.z + _orientation.right * direction.x;
        }
    }
}
