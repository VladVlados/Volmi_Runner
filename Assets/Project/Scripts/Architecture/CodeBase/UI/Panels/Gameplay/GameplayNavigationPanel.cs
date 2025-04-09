using System;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay {
  public class GameplayNavigationPanel : UIPanel {
    public event Action OnPlayButtonClick;

    [SerializeField]
    private Button _playButton;

    private void Awake() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    private void AddListeners() {
      _playButton.onClick.AddListener(PlayButtonClick);
    }

    private void PlayButtonClick() {
      OnPlayButtonClick?.Invoke();
    }

    private void RemoveListeners() {
      _playButton.onClick.AddListener(PlayButtonClick);
    }
  }
}