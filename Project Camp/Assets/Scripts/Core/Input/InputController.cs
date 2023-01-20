using UnityEngine;
using System;

namespace Core.Input
{
    public class InputController
    {
        public Action<Vector3> MoveButtonPressed;
        public Action JumpButtonPressed;
        public Action FlyButtonPressed;

        private float _flyingActivationWindow;

        public InputController(float flyingActivationWindow)
        {
            _flyingActivationWindow = flyingActivationWindow;
        }

        public void Tick(float deltaTime)
        {
            // check move input
            // check jump input
            // check fly input
        }
    }
}
