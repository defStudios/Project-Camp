using BeaconProject.Core.Factories;
using UnityEngine;

namespace BeaconProject.Core.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(StateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }
        
        public void Enter(string sceneName)
        {
            // show loading screen
            _sceneLoader.Load(sceneName, OnSceneLoaded);
        }

        public void Exit()
        {
            // hide loading screen
        }

        private void OnSceneLoaded()
        {
            var spawnPoint = GameObject.FindWithTag("SpawnPoint");
            var player = _gameFactory.CreatePlayer(spawnPoint.transform.position);

            var cam = GameObject.FindWithTag("PlayerCamera").GetComponent<Cinemachine.CinemachineVirtualCameraBase>();
            cam.Follow = player.transform;
            cam.LookAt = player.transform;
            
            // setup level environment
         
            _stateMachine.Enter<GameLoopState>();
        }
    }
}