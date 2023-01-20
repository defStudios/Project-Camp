using UnityEngine;
using Core.Input;

namespace Core.Movements
{
    public class InputMovement
    {
        private InputController _input;
        private MovementHandler _movement;
        private RotationHandler _rotation;

        public InputMovement(InputController input, MovementHandler movement, RotationHandler rotation)
        {
            _input = input;
            _movement = movement;
            _rotation = rotation;

            _input.MoveButtonPressed += OnMoveButtonPressed;
            _input.MoveButtonsReleased += OnMoveButtonsReleased;
            _input.JumpButtonPressed += OnJumpButtonPressed;
            _input.FlyButtonPressed += OnFlyingButtonPressed;
        }

        private void OnMoveButtonPressed(Vector3 direction)
        {
            _movement.Move(direction);
            _rotation.Rotate(direction);
        }

        private void OnMoveButtonsReleased()
        {
            _movement.Move(Vector3.zero);
        }

        private void OnJumpButtonPressed()
        {
            _movement.TryJump();
        }

        private void OnFlyingButtonPressed()
        {
            _movement.ToggleFlyingMode();
        }
    }
}
