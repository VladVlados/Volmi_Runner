using Project.Scripts.Architecture.CodeBase.Services;

namespace Project.Scripts.Architecture.CodeBase.GameStates {
  public interface IGameStateMachine: IService  {
    void Enter<TState>() where TState : class, IState;
  }
}