using UnityEngine;
using Cinemachine;

namespace Core.Cameras
{
    public class CameraFollower : MonoBehaviour
    {
        public Camera Camera => _camera;

        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCameraBase cam;

        public void SetTarget(Transform target)
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            cam.Follow = target;
            cam.LookAt = target;
        }
    }
}
