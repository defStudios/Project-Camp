using System;

namespace BeaconProject.Core
{
	public class Game
	{
		public StateMachine StateMachine { get; private set; }

		public Game(ICoroutineRunner coroutineRunner)
		{
			StateMachine = new StateMachine(new SceneLoader(coroutineRunner));
		}
	}
}

