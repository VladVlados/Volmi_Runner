using Project.Scripts.Architecture.CodeBase.GameStates.States.Gameplay;
using Project.Scripts.Architecture.CodeBase.UI;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Lobby;

namespace Project.Scripts.Architecture.CodeBase.GameStates.States.Lobby {
  public class LobbySceneState : State {
    private readonly IUIManager _uiManager;
    
    private LobbyNavigationPanel _lobbyNavigationPanel;

    public LobbySceneState(IUIManager uiManager) {
      _uiManager = uiManager;
    }

    public override void Enter() {
      AddListeners();
      _uiManager.Show<LobbyNavigationPanel>();
    }

    public override void Exit() {
      RemoveListeners();
      base.Exit();
    }

    private void AddListeners() {
      LobbyNavigationPanel.OnPlayButtonClick += OnLobbyNavigationPanelClick;
    }

    private void RemoveListeners() {
      LobbyNavigationPanel.OnPlayButtonClick -= OnLobbyNavigationPanelClick;
    }

    private void OnLobbyNavigationPanelClick() {
      _gameStateMachine.Enter<GameplayLoadingState>();
      IsActive = false;
    }

    private LobbyNavigationPanel LobbyNavigationPanel {
      get {
        return _lobbyNavigationPanel ??= _uiManager.GetPanel<LobbyNavigationPanel>();
      }
    }
  }
}