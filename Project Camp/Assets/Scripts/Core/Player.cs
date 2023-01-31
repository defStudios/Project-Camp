using Collectibles;
using Core.Collisions;
using Core.Movements;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour
    {
        public int ArtifactsCount { get; private set; }

        public System.Action OnArtifactGot;

        [SerializeField] private Data.PlayerConfig config;

        [Space]
        [SerializeField] private float height;
        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private Transform model;
        [SerializeField] private Transform orientation;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform groundChecker;

        [Space]
        [SerializeField] private CollisionHandler collisions;

        private Input.InputController _input;
        private MovementHandler _movement;
        private FlightHandler _flight;
        private RotationHandler _rotation;
        private InputMovement _inputMovement;

        private List<Artifact> _artifacts;

        private void Start()
        {
            var camTransf = Camera.main.transform;

            _input = new Input.InputController(camTransf, config.FlyingActivationWindow);

            _movement = new MovementHandler(transform, orientation, rb,
                groundChecker, config.GroundDrag, groundLayers, 
                config.MoveSpeed, config.JumpForce, config.JumpAirMultiplier);

            _flight = new FlightHandler(orientation, rb, config.FlightSpeed, config.FlightDrag);

            _rotation = new RotationHandler(transform, model, orientation,
                config.RotationSpeed);

            _inputMovement = new InputMovement(_input, _movement, _flight, _rotation);

            _artifacts = new List<Artifact>();
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

        public void CollectArtifact(Collectibles.Artifact artifact)
        {
            if (_artifacts.Contains(artifact))
                return;

            _artifacts.Add(artifact);

            ArtifactsCount++;
            OnArtifactGot?.Invoke();
        }
    }
}
