using System.Collections.Generic;
using System;

namespace BeaconProject.Core
{
	public class StateMachine
	{
		private readonly Dictionary<Type, IExitableState> _states;
		private IExitableState _activeState;

		public StateMachine(SceneLoader sceneLoader)
		{
			_states = new Dictionary<Type, IExitableState>()
			{
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
				[typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
				[typeof(GameLoopState)] = new GameLoopState(this),
			};
		}

		public void Enter<TState>() where TState: class, IState
		{
			var state = ChangeState<TState>();
			state.Enter();
		}		
		
		public void Enter<TState, TPayload>(TPayload data) where TState: class, IPayloadedState<TPayload>
		{
			var state = ChangeState<TState>();
			state.Enter(data);
		}

		private TState ChangeState<TState>() where TState: class, IExitableState
		{
			_activeState?.Exit();
			
			var state = _states[typeof(TState)] as TState;
			_activeState = state;

			return state;
		}
	}
}
