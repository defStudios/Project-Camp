using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCameraBase vCamera;

        [Space]
        [SerializeField] private Player player;
        [SerializeField] private Vector3 spawnPosition;

        private void Awake()
        {
            return;
            var playerInst = Instantiate(player, spawnPosition, Quaternion.identity);

            vCamera.Follow = playerInst.transform;
            vCamera.LookAt = playerInst.transform;
        }
    }
}
