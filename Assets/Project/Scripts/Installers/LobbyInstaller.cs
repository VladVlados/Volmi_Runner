using Project.Scripts.Architecture.CodeBase.Services.Lobby;
using Zenject;

namespace Project.Scripts.Installers {
  public class LobbyInstaller : MonoInstaller<MainGameInstaller> {
    public override void InstallBindings() {
      var environmentHandler = Container.Instantiate<LobbyEnvironmentHandler>();
      Container.Bind<ILobbyEnvironmentHandler>().FromInstance(environmentHandler).AsSingle().NonLazy();
      Container.Instantiate<LobbySettingsHandler>();
    }
  }
}