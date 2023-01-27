namespace BeaconProject.Core
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(StateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
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
            // instantiate player
            // setup level environment
            // setup camera
         
            _stateMachine.Enter<GameLoopState>();
        }
    }
}