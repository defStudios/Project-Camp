using InputModule = UnityEngine.Input;
using UnityEngine;
using System;

namespace Core.Input
{
    public class InputController
    {
        public Action<Vector3> MoveButtonPressed;
        public Action MoveButtonsReleased;
        public Action JumpButtonPressed;
        public Action FlyButtonPressed;

        private float _flyingActivationWindowDuration;
        private float _currentFlyingActivationWindow;

        public InputController(float flyingActivationWindowDuration)
        {
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

            if (moveDirection != Vector3.zero)
                MoveButtonPressed?.Invoke(moveDirection);
            else
                MoveButtonsReleased?.Invoke();

            if (_currentFlyingActivationWindow > 0)
                _currentFlyingActivationWindow -= deltaTime;

            if (InputModule.GetKeyDown(KeyCode.Space))
            {
                if (_currentFlyingActivationWindow > 0)
                {
                    FlyButtonPressed?.Invoke();
                    _currentFlyingActivationWindow = 0;
                }
                else
                {
                    JumpButtonPressed?.Invoke();
                    _currentFlyingActivationWindow = _flyingActivationWindowDuration;
                }
            }
        }
    }
}
