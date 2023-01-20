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
        [SerializeField] private Transform model;
        [SerializeField] private Transform orientation;
        [SerializeField] private Rigidbody rb;

        [Space]
        [SerializeField] private Cinemachine.CinemachineVirtualCameraBase vcam;
        [SerializeField] private CollisionHandler collisions;

        private Input.InputController _input;
        private MovementHandler _movement;
        private FlightHandler _flight;
        private RotationHandler _rotation;
        private InputMovement _inputMovement;

        private void Start()
        {
            var camTransf = Camera.main.transform;

            _input = new Input.InputController(camTransf, config.FlyingActivationWindow);

            _movement = new MovementHandler(transform, orientation, rb,
                height, config.GroundDrag, groundLayers, 
                config.MoveSpeed, config.JumpForce, config.JumpAirMultiplier);
            _movement.Enable();

            _flight = new FlightHandler(transform, orientation, rb,
                config.FlightSpeed, config.FlightDrag, height, groundLayers);

            _rotation = new RotationHandler(transform, model, orientation,
                config.RotationSpeed);

            _inputMovement = new InputMovement(_input, _movement, _flight, _rotation);
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
            _flight.FixedTick(delta);
        }
    }
}
