using UnityEngine;
using Core.Input;

namespace Core.Movements
{
    public class InputMovement
    {
        private InputController _input;
        private MovementHandler _movement;
        private FlightHandler _flight;
        private RotationHandler _rotation;

        public InputMovement(InputController input, MovementHandler movement, FlightHandler flight, RotationHandler rotation)
        {
            _input = input;
            _movement = movement;
            _flight = flight;
            _rotation = rotation;

            _input.MoveButtonPressed += OnMoveButtonPressed;
            _input.MoveButtonsReleased += OnMoveButtonsReleased;
            _input.JumpButtonPressed += OnJumpButtonPressed;
            _input.FlyButtonPressed += OnFlyingButtonPressed;
        }

        private void OnMoveButtonPressed(Vector3 direction)
        {
            _movement.Move(direction);
            _flight.Move(direction);
            _rotation.Rotate(direction);
        }

        private void OnMoveButtonsReleased()
        {
            _movement.Move(Vector3.zero);
            _flight.Move(Vector3.zero);
        }

        private void OnJumpButtonPressed()
        {
            _movement.TryJump();
        }

        private void OnFlyingButtonPressed()
        {
            if (_flight.ModeActive)
            {
                _flight.Disable();
                _movement.Enable();
            }
            else
            {
                _flight.Enable();
                _movement.Disable();
            }
        }
    }
}
