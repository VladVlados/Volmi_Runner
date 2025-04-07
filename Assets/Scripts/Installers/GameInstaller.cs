using Architecture.CodeBase.Services.CoroutineHandler;
using Architecture.CodeBase.Services.Events;
using Architecture.CodeBase.Services.Factory;
using Architecture.CodeBase.Services.GlobalData;
using Architecture.CodeBase.Services.Save;
using Architecture.CodeBase.Services.SceneLoader;
using UnityEngine;
using Zenject;

namespace Installers {
  public class GameInstaller : MonoInstaller<GameInstaller>
  {
    public override void InstallBindings() {
      SetApplicationSettings();
      InstallGlobalData();
      InstallEventService();
      InstallSceneLoader();
      InstallGameFactory();
      InstallSavedData();
      InstallCoroutineHandler();
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
