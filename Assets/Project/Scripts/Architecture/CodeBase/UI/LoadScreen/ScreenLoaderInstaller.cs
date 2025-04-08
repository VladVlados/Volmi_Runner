using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.UI.LoadScreen {
  public class ScreenLoaderInstaller : MonoInstaller<ScreenLoaderInstaller>{
    [SerializeField]
    private ScreenLoader _screenLoader;

    public override void InstallBindings() {
      ILoaderScreenService screenLoader = Container.InstantiatePrefabForComponent<ScreenLoader>(_screenLoader);
      Container.Bind<ILoaderScreenService>().FromInstance(screenLoader);
    }
  }
}