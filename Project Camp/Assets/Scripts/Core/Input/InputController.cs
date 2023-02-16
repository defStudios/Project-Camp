using InputModule = UnityEngine.Input;
using UnityEngine;

namespace Core.Input
{
    public class InputController
    {
        public event MoveInputHandler OnMoveInput;
        public event JumpInputHandler OnJumpInputPressed;
        public event FlightModeInputHandler OnFlightModePressed;

        public delegate void MoveInputHandler(Vector3 cameraPosition, Vector3 input);
        public delegate void JumpInputHandler();
        public delegate void FlightModeInputHandler();

        private readonly float _flyingActivationWindowDuration;
        private readonly Transform _cameraTransform;

        private float _currentFlyingActivationWindow;
        private Vector3 _lastMovementDirection;

        public InputController(Transform cameraTransform, float flyingActivationWindowDuration)
        {
            _cameraTransform = cameraTransform;
            _flyingActivationWindowDuration = flyingActivationWindowDuration;
        }

        public void Tick(float deltaTime)
        {
            var moveDirection = Vector3.zero;

            if (InputModule.GetKey(KeyCode.W))
                moveDirection += Vector3.forward;
            if (InputModule.GetKey(KeyCode.S))
                moveDirection += Vector3.back;
            if (InputModule.GetKey(KeyCode.A))
                moveDirection += Vector3.left;
            if (InputModule.GetKey(KeyCode.D))
                moveDirection += Vector3.right;
            if (InputModule.GetKey(KeyCode.Space))
                moveDirection += Vector3.up;
            if (InputModule.GetKey(KeyCode.LeftShift))
                moveDirection += Vector3.down;

            if (moveDirection != Vector3.zero ||
                moveDirection != _lastMovementDirection)
            {
                OnMoveInput?.Invoke(_cameraTransform.position, moveDirection);
            }

            _lastMovementDirection = moveDirection;
            
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
        }
    }
}
