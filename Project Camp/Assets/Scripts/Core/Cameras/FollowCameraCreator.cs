using UnityEngine;
using Cinemachine;

namespace Core.Cameras
{
    public class FollowCameraCreator : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCameraBase camPrefab;

        public void CreateCamera(Transform target)
        {
            var cam = Instantiate(camPrefab);
            cam.Follow = target;
            cam.LookAt = target;
        }
    }
}
