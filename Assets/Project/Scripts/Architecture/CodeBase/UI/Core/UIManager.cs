using System;
using System.Collections.Generic;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using Project.Scripts.Architecture.CodeBase.Utils;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.UI.Core {
  public class UIManager : FactoryPrefab, IUIManager {
    private readonly List<IUIPanel> _panels = new();
    
    [SerializeField]
    private CanvasGroup _intractableGroup;

    private Dictionary<Type, IUIPanel> _panelsMap;

    public void Init(Dictionary<Type, IUIPanel> panelsMap) {
      for (int i = _panels.Count - 1; i >= 0; i--) {
        Destroy(_panels.PopLast().Instance);
      }

      _panelsMap = panelsMap;
      InitializeNotification();

      Initialized = true;
    }

    public bool Hide<T>(bool animate = true) where T : IUIPanel {
      if (_panels.Count == 0) {
        return false;
      }

      IUIPanel panelToFind = _panelsMap[typeof(T)];
      IUIPanel panel = _panels.PopFirstOrDefault(uiPanel => uiPanel == panelToFind);

      return panel != null && ClosePanel(panel, animate);
    }

    public bool Hide(IUIPanel panel, bool animate = true) {
      if (_panels.Count == 0 || !_panels.Contains(panel)) {
        return false;
      }

      _panels.Remove(panel);
      return ClosePanel(panel, animate);
    }

    public void Show<T>(int order = -1, bool animate = true, Action onOpen = null) where T : IUIPanel {
      IUIPanel panel = _panelsMap[typeof(T)];
      OpenPanel(panel, order, onOpen, animate);
    }

    public void Show(IUIPanel panel, int order = -1, bool animate = true, Action onOpen = null) {
      OpenPanel(panel, order, onOpen, animate);
    }

    public T GetPanel<T>() where T : IUIPanel {
      if (_panelsMap[typeof(T)] is T panel) {
        return panel;
      }

      Debug.LogError("Panel has wrong type " + typeof(T));
      return default;
    }

    public void EnableIntractable() {
      _intractableGroup.interactable = true;
    }

    public void DisableIntractable() {
      _intractableGroup.interactable = false;
    }

    private void InitializeNotification() {
      foreach (IUIPanel panel in _panelsMap.Values) {
        panel.OnInitialized();
      }
    }

    public void HideAll() {
      for (int i = _panels.Count - 1; i >= 0; i--) {
        IUIPanel panel = _panels.PopLast();
        panel.Close();
        panel.Instance.SetParent(ContainerHided);
      }
    }

    private bool ClosePanel(IUIPanel panel, bool animate) {
      panel.Instance.SetParent(ContainerHided);
      Action close = animate ? panel.AnimatedClose : panel.Close;
      close.Invoke();
      if (_panels.Count == 0) {
        return true;
      }

      return true;
    }

    private void OpenPanel(IUIPanel panel, int order, Action onOpen, bool animate) {
      if (_panels.Contains(panel)) {
        return;
      }

      _panels.Push(panel);
      panel.Order = order >= 0 ? order : _panels.Count;
      panel.Instance.SetParent(ContainerShowed);
      if (animate) {
        panel.AnimatedOpen(onOpen);
      } else {
        panel.Prepare();
        panel.Open();
      }
    }
    
    public bool Initialized { get; private set; }

    [field: SerializeField]
    public Transform ContainerShowed { get; private set; }

    [field: SerializeField]
    public Transform ContainerHided { get; private set; }
  }
}