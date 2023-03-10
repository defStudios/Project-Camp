using System.Collections;
using Core.Collisions;
using Core.Movements;
using Core.Services;
using UnityEngine;
using Inventory;
using GUI;

namespace Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Data.PlayerConfig config;

        [Space, SerializeField] private Transform model;
        [SerializeField] private Transform orientation;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform groundChecker;
        [SerializeField] private LayerMask groundLayers;

        [field: Space, SerializeField] public OrbitalMovement Orbit { get; private set; }

        public PlayerInventory Inventory { get; private set; }
        public Input.InputController Input { get; private set; }
        
        private InputMovement _inputMovement;
        private MovementHandler _movement;
        private FlightHandler _flight;
        private RotationHandler _rotation;

        public void Init()
        {
            var camTransf = Camera.main.transform;

            Inventory = new PlayerInventory();

            Input = new Input.InputController(camTransf, config.FlyingActivationWindow);

            _movement = new MovementHandler(transform, orientation, rb,
                groundChecker, config.GroundDrag, groundLayers, 
                config.MoveSpeed, config.JumpForce, config.JumpAirMultiplier);

            _flight = new FlightHandler(orientation, rb, config.FlightSpeed, config.FlightDrag);

            _rotation = new RotationHandler(transform, model, orientation,
                config.RotationSpeed);

            _inputMovement = new InputMovement(Input, _movement, _flight, _rotation);

            //StartCoroutine(InitMessageDelay(1));
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            Input.Tick(delta);
            _inputMovement.Tick(delta);
        }
        
        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            _inputMovement.FixedTick(delta);
        }

        public void EnableMovement()
        {
            Input.Enable();
            _movement.Enable();
        }

        public void DisableMovement()
        {
            Input.Disable();
            
            _movement.Disable();
            _movement.Stop();
        }

        private IEnumerator InitMessageDelay(float duration)
        {
            yield return new WaitForSeconds(duration);
            ServiceManager.Container.Single<IMessageHandler>().ShowMessage("Я народився!", 4, false);
            ServiceManager.Container.Single<IMessageHandler>().ShowMessage("<color=red>Свиня_В_Джакузі.png</color>", 4, false);
        }
    }
}
