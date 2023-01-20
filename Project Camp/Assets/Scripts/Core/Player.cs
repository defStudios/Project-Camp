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
        [SerializeField] private float height;
        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private Transform orientation;
        [SerializeField] private Rigidbody rb;

        [Space]
        [SerializeField] private FollowCameraCreator followCamCreator;
        [SerializeField] private CollisionHandler collisions;

        private Input.InputController _input;
        private MovementHandler _movement;
        private RotationHandler _rotation;
        private InputMovement _inputMovement;

        private void Start()
        {
            _input = new Input.InputController(config.FlyingActivationWindow);

            _movement = new MovementHandler(transform, orientation, rb, height, config.GroundDrag, groundLayers, 
                config.MoveSpeed, config.FlightSpeed, config.JumpForce, config.JumpAirMultiplier);

            _rotation = new RotationHandler(transform, orientation, Camera.main.transform, config.RotationSpeed);
            _inputMovement = new InputMovement(_input, _movement, _rotation);

            followCamCreator.CreateCamera(transform);
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            _movement.Tick(delta);
            _rotation.Tick(delta);

            _input.Tick(delta);
        }
        
        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            _movement.FixedTick(delta);    
            _rotation.FixedTick(delta);
        }
    }
}
