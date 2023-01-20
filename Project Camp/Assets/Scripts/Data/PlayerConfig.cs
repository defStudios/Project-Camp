using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Data/New Player Config", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        public float FlyingActivationWindow => flyingActivationWindow;
        public float MoveSpeed => moveSpeed;

        [Header("Input")]
        [SerializeField] private float flyingActivationWindow;

        [Header("Movement")]
        [SerializeField] private float moveSpeed;
    }
}
