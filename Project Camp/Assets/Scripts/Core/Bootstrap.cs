using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCameraBase vCamera;

        [Space]
        [SerializeField] private GUI.GUIController guiController;
        [SerializeField] Environment.LevelEnvironment environment;
        [SerializeField] private Player player;
        [SerializeField] private Vector3 spawnPosition;

        private void Awake()
        {
            var playerInst = Instantiate(player, spawnPosition, Quaternion.identity);
            environment.Init(playerInst);

            guiController.Init(playerInst, environment);

            vCamera.Follow = playerInst.transform;
            vCamera.LookAt = playerInst.transform;
        }
    }
}
