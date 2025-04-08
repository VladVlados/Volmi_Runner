using System;
using Project.Scripts.Architecture.CodeBase.Services;

namespace Project.Scripts.Architecture.CodeBase.UI.Core {
  public interface IUIManager: IService {
    public bool Hide<T>(bool animate = true) where T : IUIPanel;

    public bool Hide(IUIPanel panel, bool animate = true);

    public void Show<T>(int order = -1, bool animate = true, Action onOpen = null) where T : IUIPanel;

    public void Show(IUIPanel panel, int order = -1, bool animate = true, Action onOpen = null);

    public T GetPanel<T>() where T : IUIPanel;

    public void EnableIntractable();
    public void DisableIntractable();

    public bool Initialized { get; }
  }
}