using Project.Scripts.Architecture.CodeBase.UI;
using Project.Scripts.Architecture.CodeBase.UI.Core;

namespace Project.Scripts.Architecture.CodeBase.GameStates.States {
  public class LobbySceneState : State {
    private readonly IUIManager _uiManager;
    private LobbyNavigationPanel _lobbyNavigationPanel;

    public LobbySceneState(IUIManager uiManager)  {
      _uiManager = uiManager;
    }

    public override void Enter() {
      LobbyNavigationPanel.OnClick += OnLobbyNavigationPanelClick;
      _uiManager.Show<LobbyNavigationPanel>();
      IsActive = false;
    }

    public override void Exit() {
      LobbyNavigationPanel.OnClick -= OnLobbyNavigationPanelClick;
      base.Exit();
    }

    private void OnLobbyNavigationPanelClick() {
      _gameStateMachine.Enter<GameplaySceneState>();
    }

    private LobbyNavigationPanel LobbyNavigationPanel {
      get {
        return _lobbyNavigationPanel ??= _uiManager.GetPanel<LobbyNavigationPanel>();
      }
    }
  }
}