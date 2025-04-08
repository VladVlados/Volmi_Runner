using System;
using System.Collections.Generic;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types {
  [CreateAssetMenu(fileName = "UIPanels", menuName = "GlobalData/UIPanels")]
  public class UIPanels : ItemGlobalData {
    [SerializeField]
    private List<UIPanel> _panelsList;

    public Dictionary<Type, IUIPanel> WarmUp(DiContainer container, Transform parent) {
      foreach (UIPanel panel in _panelsList) {
        var instance = container.InstantiatePrefabForComponent<UIPanel>(panel, parent);
        instance.Close();
        instance.gameObject.SetActive(false);
        PanelsMap.Add(panel.GetType(), instance);
      }

      return PanelsMap;
    }

    private Dictionary<Type, IUIPanel> PanelsMap { get; } = new();
  }
}