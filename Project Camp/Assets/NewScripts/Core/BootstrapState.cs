using System;

namespace BeaconProject.Core
{
	public class BootstrapState : IState
	{
		private readonly StateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;

		private const string _bootStrapSceneName = "Bootstrap";

		public BootstrapState(StateMachine stateMachine, SceneLoader sceneLoader)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
		}

		public void Enter()
		{
			UnityEngine.Debug.Log("Bootstrap state entered!");
			_sceneLoader.Load(_bootStrapSceneName, EnterLoadLevel);
		}

		public void Exit()
		{

		}

		private void EnterLoadLevel()
		{

		}
	}
}

