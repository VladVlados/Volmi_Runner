using Project.Scripts.Architecture.CodeBase.Services.CoroutineHandler;
using Project.Scripts.Architecture.CodeBase.Services.Events;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using Project.Scripts.Architecture.CodeBase.Services.Save;
using Project.Scripts.Architecture.CodeBase.Services.SceneLoader;
using Project.Scripts.Architecture.CodeBase.Signal;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Installers {
  public class MainGameInstaller : MonoInstaller<MainGameInstaller> {
    public override void InstallBindings() {
      SetApplicationSettings();
      InstallCoroutineHandler();
      InstallGlobalData();
      InstallEventService();
      InstallSceneLoader();
      InstallGameFactory();
      InstallSavedData();
      
      SignalBusInstaller.Install(Container);
      Container.DeclareSignal<SceneReadySignal>();
    }

    private void SetApplicationSettings() {
      Application.targetFrameRate = 120;
      Input.multiTouchEnabled = false;
    }

    private void InstallGlobalData() {
      Container.Bind<IGlobalDataService>().To<GlobalDataService>().AsSingle();
    }

    private void InstallEventService() {
      var monoEventsProvider = Container.InstantiateComponent<MonoEventsProvider>(new GameObject(nameof(MonoEventsProvider)));
      Container.Bind<IMonoEventsService>().FromInstance(monoEventsProvider).AsSingle();
    }

    private void InstallSceneLoader() {
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
    }

    private void InstallGameFactory() {
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
      Container.Bind<IPlayerViewFactory>().To<PlayerViewFactory>().AsSingle();
      Container.Bind<ITileViewFactory>().To<TileViewFactory>().AsSingle();
      Container.Bind<IObstacleViewFactory>().To<ObstacleViewFactory>().AsSingle();
    }

    private void InstallSavedData() {
      Container.Bind<IDataProvider>().To<DataProvider>().AsSingle();
      Container.Bind<IDataLoader>().To<IDataProvider>().FromResolve();
      Container.Bind<IDataSaver>().To<IDataProvider>().FromResolve();
    }

    private void InstallCoroutineHandler() {
      var coroutineHandler = new GameObject(nameof(CoroutineHandler)).AddComponent<CoroutineHandler>();
      DontDestroyOnLoad(coroutineHandler);
      Container.Bind<ICoroutineHandler>().FromInstance(coroutineHandler).AsSingle();
    }
  }
}