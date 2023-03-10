using Core.Cameras;
using UnityEngine;

namespace Core.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly Player _player;
        private readonly CameraFollower _camera; 

        public GameFactory(Player player, CameraFollower camera)
        {
            _player = player;
            _camera = camera;
        }

        public Player CreatePlayer(Vector3 spawnPosition)
        {
            var player = Object.Instantiate(_player, spawnPosition, Quaternion.identity);
            player.Init();
            
            return player;
        }
        
        public CameraFollower CreateCamera(Transform target)
        {
            var cam = Object.Instantiate(_camera);
            cam.SetTarget(target);
            
            return cam;
        }
    }
}
