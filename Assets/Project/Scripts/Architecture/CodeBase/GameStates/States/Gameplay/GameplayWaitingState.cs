using Project.Scripts.Architecture.CodeBase.UI.Core;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Lobby;

namespace Project.Scripts.Architecture.CodeBase.GameStates.States.Gameplay {
  public class GameplayWaitingState : State {
    private readonly IUIManager _uiManager;
    
    private GameplayNavigationPanel _gameplayNavigationPanel;

    public GameplayWaitingState(IUIManager uiManager){
      _uiManager = uiManager;
    }

    public override void Enter() {
      AddListeners();
    }

    public override void Exit() {
      RemoveListeners();
      base.Exit();
    }

    private void AddListeners() {
      LobbyNavigationPanel.OnPlayButtonClick += OnPlayButtonClick;
    }

    private void RemoveListeners() {
      LobbyNavigationPanel.OnPlayButtonClick -= OnPlayButtonClick;
    }

    private void OnPlayButtonClick() {
      IsActive = false;
      _uiManager.Hide<GameplayNavigationPanel>();
      _gameStateMachine.Enter<GameplayState>();
    }

    private GameplayNavigationPanel LobbyNavigationPanel {
      get {
        return _gameplayNavigationPanel ??= _uiManager.GetPanel<GameplayNavigationPanel>();
      }
    }
  }
}