using System.Collections.Generic;
using System;

namespace BeaconProject.Core
{
	public class StateMachine
	{
		private Dictionary<Type, IState> _states;

		public StateMachine(SceneLoader sceneLoader)
		{
			_states = new Dictionary<Type, IState>()
			{
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
			};
		}

		public void Enter<T>() where T: IState
		{
			_states[typeof(T)].Enter();
		}
	}
}
