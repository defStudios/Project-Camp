using Core.Collisions;
using Core.Movements;
using UnityEngine;
using Inventory;

namespace Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Data.PlayerConfig config;

        [Space]
        [SerializeField] private Transform model;
        [SerializeField] private Transform orientation;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform groundChecker;
        [SerializeField] private LayerMask groundLayers;

        [Space]
        [SerializeField] private CollisionHandler collisions;

        public PlayerInventory Inventory { get; private set; }
        
        private Input.InputController _input;
        private InputMovement _inputMovement;
        private MovementHandler _movement;
        private FlightHandler _flight;
        private RotationHandler _rotation;

        public void Init()
        {
            var camTransf = Camera.main.transform;

            Inventory = new PlayerInventory();

            _input = new Input.InputController(camTransf, config.FlyingActivationWindow);

            _movement = new MovementHandler(transform, orientation, rb,
                groundChecker, config.GroundDrag, groundLayers, 
                config.MoveSpeed, config.JumpForce, config.JumpAirMultiplier);

            _flight = new FlightHandler(orientation, rb, config.FlightSpeed, config.FlightDrag);

            _rotation = new RotationHandler(transform, model, orientation,
                config.RotationSpeed);

            _inputMovement = new InputMovement(_input, _movement, _flight, _rotation);
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            _input.Tick(delta);
            _inputMovement.Tick(delta);
        }
        
        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            _inputMovement.FixedTick(delta);
        }
    }
}
