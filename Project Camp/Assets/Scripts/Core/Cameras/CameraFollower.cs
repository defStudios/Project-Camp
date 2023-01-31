using UnityEngine;
using Cinemachine;

namespace Core.Cameras
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCameraBase cam;

        public void SetTarget(Transform target)
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            cam.Follow = target;
            cam.LookAt = target;
        }
    }
}
