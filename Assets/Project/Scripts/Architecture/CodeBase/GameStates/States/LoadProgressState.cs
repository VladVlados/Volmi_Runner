using Zenject;

namespace Project.Scripts.Architecture.CodeBase.GameStates.States {
  public class LoadProgressState : State {
    private readonly DiContainer _container;

    public LoadProgressState(DiContainer container) {
      _container = container;
    }

    public override void Enter() {
      base.Enter();

      IsActive = false;
    }
  }
}