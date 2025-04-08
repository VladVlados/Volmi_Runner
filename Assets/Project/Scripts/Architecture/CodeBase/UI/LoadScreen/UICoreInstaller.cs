using Project.Scripts.Architecture.CodeBase.UI.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.UI.LoadScreen {
  public class UICoreInstaller : MonoInstaller<UICoreInstaller> {
    [SerializeField]
    private ScreenLoader _screenLoader;
    [SerializeField]
    private UIManager _uiManager;

    public override void InstallBindings() {
      ILoaderScreenService screenLoader = Container.InstantiatePrefabForComponent<ScreenLoader>(_screenLoader);
      Container.Bind<ILoaderScreenService>().FromInstance(screenLoader);
      IUIManager uiManager = Container.InstantiatePrefabForComponent<UIManager>(_uiManager);
      Container.Bind<IUIManager>().FromInstance(uiManager);
    }
  }
}