using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Gameplay.Meta;
using Project.Scripts.Architecture.CodeBase.Gameplay.Player;
using Project.Scripts.Architecture.CodeBase.Signal;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Installers {
  public class GameplayInstaller : MonoInstaller<GameplayInstaller> {
    [SerializeField]
    private LevelAreaSetup _areaSetup;

    public override void InstallBindings() {
      var signalBus = Container.Resolve<SignalBus>();

      ILevelAreaSetup levelAreaSetup = Container.InstantiatePrefabForComponent<LevelAreaSetup>(_areaSetup);
      Container.Bind<ILevelAreaSetup>().FromInstance(levelAreaSetup);
      
      var playerCollisionHandler = Container.Instantiate<PlayerCollisionHandler>();
      Container.Bind<IPlayerCollisionHandler>().FromInstance(playerCollisionHandler).AsSingle();
      
      var playerRewardHandler = Container.Instantiate<PlayerRewardHandler>();
      Container.Bind<IPlayerRewardHandler>().FromInstance(playerRewardHandler).AsSingle();
      
      var obstacleProvider = Container.Instantiate<ObstacleProvider>();
      Container.Bind<IObstacleProvider>().FromInstance(obstacleProvider).AsSingle();
      
      var playerController = Container.Instantiate<PlayerViewProvider>();
      Container.Bind<IPlayerViewProvider>().FromInstance(playerController).AsSingle();

      var levelGenerator = Container.Instantiate<LevelGenerator>();
      Container.Bind<ILevelGenerator>().FromInstance(levelGenerator).AsSingle();
      
      var levelMoveSimulator = Container.Instantiate<LevelMoveSimulator>();
      Container.Bind<ILevelMoveSimulator>().FromInstance(levelMoveSimulator).AsSingle();

      signalBus.Fire(new SceneReadySignal(Container));
    }
  }
}