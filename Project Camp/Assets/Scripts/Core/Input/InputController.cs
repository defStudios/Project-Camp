using InputModule = UnityEngine.Input;
using UnityEngine;

namespace Core.Input
{
    public class InputController
    {
        public event MoveInputHandler OnMoveInput;
        public delegate void MoveInputHandler(Vector3 cameraPosition, Vector3 input);

        public event JumpInputHandler OnJumpInputPressed;
        public delegate void JumpInputHandler();

        public event FlightModeInputHandler OnFlightModePressed;
        public delegate void FlightModeInputHandler();

        public event FlightInputHandler OnFlightInputPressed;
        public delegate void FlightInputHandler(Vector3 direction);

        private float _flyingActivationWindowDuration;
        private float _currentFlyingActivationWindow;

        private Transform _cameraTransform;
        private Vector3 _lastMovementDirection;

        public InputController(Transform cameraTransform, float flyingActivationWindowDuration)
        {
            _cameraTransform = cameraTransform;
            _flyingActivationWindowDuration = flyingActivationWindowDuration;
        }

        public void Tick(float deltaTime)
        {
            Vector3 moveDirection = Vector3.zero;

            if (InputModule.GetKey(KeyCode.W))
                moveDirection += Vector3.forward;
            if (InputModule.GetKey(KeyCode.S))
                moveDirection += Vector3.back;
            if (InputModule.GetKey(KeyCode.A))
                moveDirection += Vector3.left;
            if (InputModule.GetKey(KeyCode.D))
                moveDirection += Vector3.right;

            if (moveDirection != Vector3.zero ||
                moveDirection != _lastMovementDirection)
            {
                OnMoveInput?.Invoke(_cameraTransform.position, moveDirection);
            }

            if (_currentFlyingActivationWindow > 0)
                _currentFlyingActivationWindow -= deltaTime;

            if (InputModule.GetKeyDown(KeyCode.Space))
            {
                if (_currentFlyingActivationWindow > 0)
                {
                    OnFlightModePressed?.Invoke();
                    _currentFlyingActivationWindow = 0;
                }
                else
                {
                    OnJumpInputPressed?.Invoke();
                    _currentFlyingActivationWindow = _flyingActivationWindowDuration;
                }
            }

            if (InputModule.GetKey(KeyCode.Space))
                OnFlightInputPressed?.Invoke(Vector3.up);
            if (InputModule.GetKey(KeyCode.LeftShift))
                OnFlightInputPressed?.Invoke(Vector3.down);

            _lastMovementDirection = moveDirection;
        }
    }
}
