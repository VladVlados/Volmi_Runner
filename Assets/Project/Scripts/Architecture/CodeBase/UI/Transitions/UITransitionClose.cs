using System;
using Project.Scripts.Architecture.CodeBase.UI.Core;

namespace Project.Scripts.Architecture.CodeBase.UI.Transitions {
  public class UITransitionClose : UITransition {
    public override void Transit(IUIPanel panel, Action onComplete = null) {
      panel.Close();
    }
  }
}