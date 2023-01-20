using UnityEngine;
using Core.Input;

namespace Core.Movements
{
    public class InputMovement
    {
        private InputController _input;
        private MovementHandler _movement;

        public InputMovement(InputController input, MovementHandler movement)
        {
            _input = input;
            _movement = movement;

            _input.MoveButtonPressed += OnMoveButtonPressed;
            _input.JumpButtonPressed += OnJumpButtonPressed;
            _input.FlyButtonPressed += OnFlyingButtonPressed;
        }

        private void OnMoveButtonPressed(Vector3 direction)
        {
            _movement.Move(direction);
        }

        private void OnJumpButtonPressed()
        {
            _movement.Jump();
        }

        private void OnFlyingButtonPressed()
        {
            _movement.ToggleFlyingMode();
        }
    }
}
