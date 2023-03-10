using Core;
using DG.Tweening;
using UnityEngine;

namespace Environment
{
    public class Lighthouse : MonoBehaviour
    {
        [field: SerializeField] public Transform LiftPlayerPosition { get; private set; }
        
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Transform lampRotationOrigin;
        [SerializeField] private Transform lamp;
        [SerializeField] private GameObject lampLight;
        
        [Space, SerializeField] private Transform lift;
        [SerializeField] private Vector3 liftDownPosition;
        [SerializeField] private Vector3 liftUpPosition;
        [SerializeField] private Transform liftPlayerStickPosition;
        
        private bool lampActive;

        private void Start()
        {
            TurnOffLights();
            LiftUp(0);
        }

        private void Update()
        {
            if (lampActive)
            {
                lamp.RotateAround(lampRotationOrigin.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
        
        public void LiftUp(float duration, Transform playerTransform = null)
        {
            lift.DOLocalMove(liftUpPosition, duration).SetEase(Ease.Linear).OnUpdate(() =>
            {
                if (playerTransform != null)
                    playerTransform.position = liftPlayerStickPosition.position;
            }).OnComplete(() =>
            {
                if (playerTransform != null)
                    playerTransform.position = liftPlayerStickPosition.position;
            });
        }   

        public void LiftDown(float duration, Transform playerTransform = null)
        {
            lift.DOLocalMove(liftDownPosition, duration).SetEase(Ease.Linear).OnUpdate(() =>
            {
                if (playerTransform != null)
                    playerTransform.position = liftPlayerStickPosition.position;
            })
            .OnComplete(() =>
            {
                if (playerTransform != null)
                    playerTransform.position = LiftPlayerPosition.position;
            });
        }

        public void TurnOnLights()  
        {
            lampActive = true;
            lampLight.SetActive(true);
        }

        public void TurnOffLights()
        {
            lampActive = false;
            lampLight.SetActive(false);
        }
    }
}
