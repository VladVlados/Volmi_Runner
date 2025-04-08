using System;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.UI.Transitions {
  public abstract class UITransition : MonoBehaviour {
    protected IUIManager _uiManager;
    public abstract void Transit(IUIPanel panel, Action onComplete = null);

    public void Construct(IUIManager uiManager) {
      _uiManager = uiManager;
    }
  }
}