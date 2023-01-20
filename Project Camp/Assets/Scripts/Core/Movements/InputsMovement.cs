using UnityEngine;
using Core.Input;

namespace Core.Movements
{
    public class InputMovement
    {
        private InputController _input;

        private IMoveable _currentHandler;

        private MovementHandler _movement;
        private FlightHandler _flight;
        private RotationHandler _rotation;

        public InputMovement(InputController input, MovementHandler movement, FlightHandler flight, RotationHandler rotation)
        {
            _input = input;
            _movement = movement;
            _flight = flight;
            _rotation = rotation;

            _input.OnMoveInput += OnMoveButtonPressed;
            _input.OnJumpInputPressed += OnJumpButtonPressed;
            _input.OnFlightModePressed += OnFlyingButtonPressed;

            SetCurrentHandler(_movement);
        }

        public void Tick(float deltaTime)
        {
            if (_currentHandler != _movement && _movement.IsOnGround())
                SetCurrentHandler(_movement);

            _currentHandler.Tick(deltaTime);
            _rotation.Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            _currentHandler.FixedTick(deltaTime);
        }

        private void OnMoveButtonPressed(Vector3 camPosition, Vector3 moveDirection)
        {
            _currentHandler.Move(camPosition, moveDirection);
            _rotation.Rotate(camPosition, moveDirection);
        }

        private void OnJumpButtonPressed()
        {
            if (_currentHandler == _movement)
                _movement.TryJump();
        }

        private void OnFlyingButtonPressed()
        {
            SetCurrentHandler(_currentHandler == _movement ? _flight : _movement);
        }

        private void SetCurrentHandler(IMoveable handler)
        {
            _currentHandler = handler;
            _currentHandler.Enable();
        }
    }
}
