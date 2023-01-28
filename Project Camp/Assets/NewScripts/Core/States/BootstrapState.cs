using BeaconProject.Core.AssetManagement;
using BeaconProject.Core.Factories;
using BeaconProject.Core.Services;
using System;

namespace BeaconProject.Core.States
{
	public class BootstrapState : IState
	{
		private const string BootstrapSceneName = "Bootstrap";
		private const string MainSceneName = "Main";

		private readonly StateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private AllServices _services;
		
		public BootstrapState(StateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_services = services;
			
			RegisterServices();
		}

		public void Enter()
		{
			_sceneLoader.Load(BootstrapSceneName, EnterLoadLevel);
		}

		public void Exit()
		{

		}

		private void RegisterServices()
		{
			AllServices.Container.RegisterSingle<IAssets>(new Assets());
			AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssets>()));
		}

		private void EnterLoadLevel()
		{
			_stateMachine.Enter<LoadLevelState, string>(MainSceneName);
		}
	}
}

