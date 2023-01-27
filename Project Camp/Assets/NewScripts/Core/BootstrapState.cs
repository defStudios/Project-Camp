using System;

namespace BeaconProject.Core
{
	public class BootstrapState : IState
	{
		private readonly StateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;

		private const string BootstrapSceneName = "Bootstrap";
		private const string MainSceneName = "Main";

		public BootstrapState(StateMachine stateMachine, SceneLoader sceneLoader)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
		}

		public void Enter()
		{
			_sceneLoader.Load(BootstrapSceneName, EnterLoadLevel);
		}

		public void Exit()
		{

		}

		private void EnterLoadLevel()
		{
			_stateMachine.Enter<LoadLevelState, string>(MainSceneName);
		}
	}
}

