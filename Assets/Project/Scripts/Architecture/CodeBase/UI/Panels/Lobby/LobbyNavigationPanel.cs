using System;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Lobby.MatchResults;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Architecture.CodeBase.UI.Panels.Lobby {
  public class LobbyNavigationPanel : UIPanel {
    public event Action OnPlayButtonClick;

    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private Button _historyButton;
    [SerializeField]
    private Button _settingsButton;

    private void Awake() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    private void AddListeners() {
      _playButton.onClick.AddListener(PlayButtonClick);
      _historyButton.onClick.AddListener(HistoryButtonClick);
      _settingsButton.onClick.AddListener(SettingsButtonClick);
    }

    private void SettingsButtonClick() {
      UiManager.Show<LobbySettingsPanel>();
    }

    private void HistoryButtonClick() {
      UiManager.Show<MatchResultsPanel>();
    }

    private void PlayButtonClick() {
      OnPlayButtonClick?.Invoke();
    }

    private void RemoveListeners() {
      _playButton.onClick.RemoveListener(PlayButtonClick);
      _historyButton.onClick.RemoveListener(HistoryButtonClick);
      _settingsButton.onClick.RemoveListener(SettingsButtonClick);
    }
  }
}