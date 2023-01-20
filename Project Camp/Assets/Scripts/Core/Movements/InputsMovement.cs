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

            _input.OnMoveInput += OnMoveButtonPressed;
            _input.OnJumpInputPressed += OnJumpButtonPressed;

            _input.OnFlightModePressed += OnFlyingButtonPressed;
            _input.OnFlightInputPressed += OnFlightInputPressed;
        }

        private void OnMoveButtonPressed(Vector3 camPosition, Vector3 moveDirection)
        {
            _movement.Move(moveDirection);
            _flight.Move(camPosition, moveDirection);

            _rotation.Rotate(camPosition, moveDirection);
        }

        private void OnJumpButtonPressed()
        {
            _movement.TryJump();
        }

        private void OnFlyingButtonPressed()
        {
            // ???
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

        private void OnFlightInputPressed(Vector3 moveDirection)
        {
            _flight.ChangeAltitude(moveDirection);
        }
    }
}
