using UnityEngine;

namespace BeaconProject.Core.States
{
    public class GameLoopState : IState
    {
        private readonly StateMachine _stateMachine;

        public GameLoopState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            Debug.Log("Game Loop is live!");
        }
        
        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}