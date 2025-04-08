using Project.Scripts.Architecture.CodeBase.GameStates;
using Zenject;

namespace Project.Scripts.Installers {
  public class StateMachineInstaller : MonoInstaller<StateMachineInstaller> {
    public override void InstallBindings() {
      InstallStateMachine();
    }

    private void InstallStateMachine() {
      var gameStateMachine = Container.Instantiate<GameStateMachine>();
      Container.Bind<IGameStateMachine>().FromInstance(gameStateMachine).AsSingle();
    }
  }
}