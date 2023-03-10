using InputModule = UnityEngine.Input;
using UnityEngine;

namespace Core.Input
{
    public class InputController
    {
        public event MoveInputHandler OnMoveInput;
        public event JumpInputHandler OnJumpInputPressed;
        public event FlightModeInputHandler OnFlightModePressed;

        public event InteractionActivenessHandler OnInteractionStateChanged;
        public event InteractionProgressHandler OnInteractionProgressChanged;
        public event InteractionInterruptionHandler OnInteractionInterrupted;
        public event InteractionCompletionHandler OnInteractionCompleted;

        public delegate void MoveInputHandler(Vector3 cameraPosition, Vector3 input);
        public delegate void JumpInputHandler();
        public delegate void FlightModeInputHandler();

        public delegate void InteractionActivenessHandler(Transform origin, bool enabled);
        public delegate void InteractionProgressHandler(float progress);
        public delegate void InteractionCompletionHandler();
        public delegate void InteractionInterruptionHandler();
        
        private readonly float _flyingActivationWindowDuration;
        private readonly Transform _cameraTransform;

        private float _currentFlyingActivationWindow;
        private Vector3 _lastMovementDirection;

        private bool _interactionActive;
        private float _currentInteractionDuration;
        private float _currentInteractionProgress;

        private bool _enabled;

        public InputController(Transform cameraTransform, float flyingActivationWindowDuration)
        {
            _cameraTransform = cameraTransform;
            _flyingActivationWindowDuration = flyingActivationWindowDuration;
        }

        public void Tick(float deltaTime)
        {
            if (!_enabled)
                return;
            
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

            if (InputModule.GetKey(KeyCode.E))
            {
                if (_interactionActive)
                {
                    _currentInteractionProgress += deltaTime;
                    
                    if (_currentInteractionProgress > .99f)
                    {
                        OnInteractionCompleted?.Invoke();
                        ResetInteraction();
                    }
                    else
                        OnInteractionProgressChanged?.Invoke(_currentInteractionProgress);
                }
                else if (_currentInteractionProgress > 0)
                {
                    OnInteractionInterrupted?.Invoke();
                    ResetInteraction();
                }
            }
            else if (InputModule.GetKeyUp(KeyCode.E) && _interactionActive)
            {
                OnInteractionInterrupted?.Invoke();
                ResetInteraction();
            }
        }

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }
        
        public void EnableInteraction(Transform origin, float duration)
        {
            _interactionActive = true;
            _currentInteractionDuration = duration;
            
            OnInteractionStateChanged?.Invoke(origin, true);
        }

        public void DisableInteraction()
        {
            if (_currentInteractionProgress > 0)
                OnInteractionInterrupted?.Invoke();
            
            ResetInteraction();
            
            _interactionActive = false;
            
            OnInteractionStateChanged?.Invoke(null, false);
        }

        private void ResetInteraction()
        {
            _currentInteractionProgress = 0;
        }
    }
}
