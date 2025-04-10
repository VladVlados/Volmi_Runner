using Project.Scripts.Architecture.CodeBase.Gameplay.Player;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;
using Project.Scripts.Architecture.CodeBase.Services.Save;
using Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Lobby;

namespace Project.Scripts.Architecture.CodeBase.Services.Lobby {
  public class LobbySettingsHandler {
    private readonly LobbySettingsPanel _lobbySettingsPanel;

    private readonly PlayerProgressData _playerProgress;
    private readonly PlayerCustomizationData _playerCustomization;

    public LobbySettingsHandler(IUIManager uiManager, IDataProvider dataProvider) {
      _lobbySettingsPanel = uiManager.GetPanel<LobbySettingsPanel>();
      _playerProgress = dataProvider.GetSaveData().GetData<PlayerProgressData>();
      _playerCustomization = dataProvider.GetSaveData().GetData<PlayerCustomizationData>();
      _lobbySettingsPanel.SetupConfigurationOptions(_playerProgress.ConfigurationType);
      _lobbySettingsPanel.SetupPlayerViewOptions(_playerCustomization.PlayerView);
      AddListeners();
    }

    ~LobbySettingsHandler() {
      RemoveListeners();
    }

    private void AddListeners() {
      _lobbySettingsPanel.OnOnConfigurationChanged += OnOnConfigurationChanged;
      _lobbySettingsPanel.OnPlayerViewChanged += OnPlayerViewChanged;
    }

    private void RemoveListeners() {
      _lobbySettingsPanel.OnOnConfigurationChanged += OnOnConfigurationChanged;
      _lobbySettingsPanel.OnPlayerViewChanged += OnPlayerViewChanged;
    }

    private void OnPlayerViewChanged(PlayerViewType playerView) {
      _playerCustomization.SelectPlayerView(playerView);
    }

    private void OnOnConfigurationChanged(ConfigurationType configurationType) {
      _playerProgress.SetLevelConfiguration(configurationType);
    }
  }
}