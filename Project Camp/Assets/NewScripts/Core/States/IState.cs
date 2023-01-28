namespace BeaconProject.Core.States
{
    public interface IExitableState
    {
        public void Exit();
    }
    
    public interface IState : IExitableState
    {
        public void Enter();
    }    
    
    public interface IPayloadedState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}
