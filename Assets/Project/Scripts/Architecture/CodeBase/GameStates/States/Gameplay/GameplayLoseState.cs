using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Gameplay.Meta;
using Project.Scripts.Architecture.CodeBase.GameStates.States.Lobby;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay;

namespace Project.Scripts.Architecture.CodeBase.GameStates.States.Gameplay {
  public class GameplayLoseState : State {
    private readonly IUIManager _uiManager;

    private IPlayerRewardHandler _playerRewardHandler;
    private ILevelMoveSimulator _levelMoveSimulator;
    private LosePanel _losePanel;

    public GameplayLoseState(IUIManager uiManager) {
      _uiManager = uiManager;
    }

    public override void Enter() {
      base.Enter();
      SceneResolve();
      _levelMoveSimulator.StopMove();
      LosePanel.SetupResult(_playerRewardHandler.Result);
      LosePanel.OnLobbyButtonClick += OnLobbyButtonClick;
      _uiManager.Show<LosePanel>();
    }

    public override void Exit() {
      base.Exit();
      _playerRewardHandler.SaveMatchResult();
      _uiManager.HideAll();
    }

    private void OnLobbyButtonClick() {
      LosePanel.OnLobbyButtonClick -= OnLobbyButtonClick;
      _gameStateMachine.Enter<LobbyLoadingState>();
    }

    private void SceneResolve() {
      _playerRewardHandler = _container.Resolve<IPlayerRewardHandler>();
      _levelMoveSimulator = _container.Resolve<ILevelMoveSimulator>();
    }

    private LosePanel LosePanel {
      get {
        return _losePanel ??= _uiManager.GetPanel<LosePanel>();
      }
    }
  }
}