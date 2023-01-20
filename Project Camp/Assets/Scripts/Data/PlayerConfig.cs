using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Data/New Player Config", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        public float FlyingActivationWindow => flyingActivationWindow;

        public float MoveSpeed => moveSpeed;
        public float FlightSpeed => flightSpeed;
        public float RotationSpeed => rotationSpeed;
        public float GroundDrag => groundDrag;

        public float JumpForce => jumpForce;
        public float JumpAirMultiplier => jumpAirMultiplier;

        [Header("Input")]
        [SerializeField] private float flyingActivationWindow;

        [Header("Movement")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float flightSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float groundDrag;
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpAirMultiplier;
    }
}
