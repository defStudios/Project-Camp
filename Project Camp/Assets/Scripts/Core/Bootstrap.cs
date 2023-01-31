using Core.Factories;
using Core.Cameras;
using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        public static Game Game { get; private set; }
        
        [Space]
        [SerializeField] private Player player;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private CameraFollower cameraFollower;
        
        [Space]
        [SerializeField] Environment.LevelEnvironment environment;
        [SerializeField] private GUI.GUIController guiController;

        private void Awake()
        {
            var factory = new GameFactory(player, cameraFollower);
            Game = new Game(factory, environment, guiController, spawnPoint.position);
        }
    }
}
