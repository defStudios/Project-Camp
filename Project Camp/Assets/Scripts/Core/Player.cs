using System.Collections.Generic;
using Core.Collisions;
using Core.Movements;
using Core.Cameras;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Data.PlayerConfig config;

        [Space]
        [SerializeField] private Rigidbody rb;

        [Space]
        [SerializeField] private FollowCameraCreator followCamCreator;
        [SerializeField] private CollisionHandler collisions;

        private Input.InputController _input;
        private MovementHandler _movement;
        private InputMovement _inputMovement;

        private void Start()
        {
            _input = new Input.InputController(config.FlyingActivationWindow);
            _movement = new MovementHandler(transform, rb, config.MoveSpeed);
            _inputMovement = new InputMovement(_input, _movement);

            followCamCreator.CreateCamera(transform);
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            _input.Tick(delta);
            _movement.Tick(delta);
        }
    }
}
