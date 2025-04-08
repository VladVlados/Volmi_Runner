namespace Project.Scripts.Architecture.CodeBase.GameStates {
  public interface IState : IExitState {
    void Update();
    public Transition GetAvailableTransitions();
    void Enter();
    void PhysicsUpdate();
    bool IsActive { get; }
    void Setup(IGameStateMachine gameStateMachine);
  }
}