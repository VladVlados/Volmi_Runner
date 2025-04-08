using System;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.UI.Core {
  public interface IUIPanel {
    public void AnimatedOpen(Action onOpen);

    public void AnimatedClose();
    
    public void Prepare();

    public void Open();

    public void Close();
    
    public void OnInitialized();
    
    public RectTransform Instance { get; }
    public int Order { get; set; }
  }
}