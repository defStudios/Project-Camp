using UnityEngine;

namespace Utils
{
    public class Shaker : MonoBehaviour
    {
        [SerializeField] private Transform target;
        
        [Space]
        [SerializeField] private Vector3 shakeAxis;
        [SerializeField] private float shakePower;
        [SerializeField] private float shakeFrequency;
        
        private void Update()
        {
            float shakeValue = Mathf.Sin(Time.time * shakePower) * shakeFrequency * Time.deltaTime;
            var shake = shakeAxis * shakeValue;
            
            target.position += shake;
        }
    }
}
