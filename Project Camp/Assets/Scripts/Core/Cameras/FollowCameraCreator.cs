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

            //cam.OnTargetObjectWarped(target, new Vector3(17.65378f, 8.176129f, -9.818289f));
        }
    }
}
