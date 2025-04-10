using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Architecture.CodeBase.Gameplay.Player;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Architecture.CodeBase.UI.Panels.Lobby {
  public class LobbySettingsPanel : UIPanel {
    public event Action<PlayerViewType> OnPlayerViewChanged;
    public event Action<ConfigurationType> OnOnConfigurationChanged;

    [SerializeField]
    private TMP_Dropdown _playerViewDropdown;
    [SerializeField]
    private TMP_Dropdown _configurationDropdown;
    [SerializeField]
    private Button _closeButton;

    private void Awake() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    public void SetupPlayerViewOptions(PlayerViewType selectedView) {
      List<string> options = Enum.GetNames(typeof(PlayerViewType)).ToList();

      _playerViewDropdown.ClearOptions();
      _playerViewDropdown.AddOptions(options);
      _playerViewDropdown.value = (int)selectedView;
      _playerViewDropdown.RefreshShownValue();
      _playerViewDropdown.onValueChanged.AddListener(OnPlayerViewValueChanged);
    }

    public void SetupConfigurationOptions(ConfigurationType selectedConfiguration) {
      List<string> options = Enum.GetNames(typeof(ConfigurationType)).ToList();

      _configurationDropdown.ClearOptions();
      _configurationDropdown.AddOptions(options);
      _configurationDropdown.value = (int)selectedConfiguration;
      _configurationDropdown.RefreshShownValue();
      _configurationDropdown.onValueChanged.AddListener(OnConfigurationValueChanged);
    }

    private void OnPlayerViewValueChanged(int index) {
      var selectedView = (PlayerViewType)index;
      OnPlayerViewChanged?.Invoke(selectedView);
    }

    private void OnConfigurationValueChanged(int index) {
      var selectedView = (ConfigurationType)index;
      OnOnConfigurationChanged?.Invoke(selectedView);
    }

    private void AddListeners() {
      _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick() {
      UiManager.Hide(this);
    }

    private void RemoveListeners() {
      _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }
  }
}