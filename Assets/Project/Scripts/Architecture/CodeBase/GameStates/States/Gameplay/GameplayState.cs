using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Gameplay.Meta;
using Project.Scripts.Architecture.CodeBase.Gameplay.Player;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;
using Project.Scripts.Architecture.CodeBase.Services.Save;
using Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay;

namespace Project.Scripts.Architecture.CodeBase.GameStates.States.Gameplay {
  public class GameplayState : State {
    private readonly IGlobalDataService _globalDataService;
    private readonly IDataProvider _dataProvider;
    private readonly IUIManager _uiManager;
    
    private ILevelMoveSimulator _levelMoveSimulator;
    private IPlayerCollisionHandler _playerCollisionHandler;
    private IPlayerMoveController _playerMoveController;
    
    public GameplayState(IGlobalDataService globalDataService, IDataProvider dataProvider, IUIManager uiManager) {
      _globalDataService = globalDataService;
      _dataProvider = dataProvider;
      _uiManager = uiManager;
    }

    public override void Enter() {
      base.Enter();
      SceneResolve();
      ConfigurationType configuration = _dataProvider.GetSaveData().GetData<PlayerProgressData>().ConfigurationType;
      ConfigurationInfo configurationInfo = _globalDataService.GetGlobalData<LevelConfigurations>().GetConfiguration(configuration);

      _levelMoveSimulator.StartMove(configurationInfo);
      _playerMoveController.StartMoveInput();
      _uiManager.Show<GameplayProgressPanel>();
    }

    public override void Exit() {
      base.Exit();
      _playerMoveController.StopMoveInput();
      _uiManager.Hide<GameplayProgressPanel>();
    }

    private void SceneResolve() {
      _levelMoveSimulator = _container.Resolve<ILevelMoveSimulator>();
      _playerCollisionHandler = _container.Resolve<IPlayerCollisionHandler>();
      _playerMoveController = _container.Resolve<IPlayerMoveController>();
      _playerCollisionHandler.OnObstacleCollision += OnObstacleCollision;
    }

    private void OnObstacleCollision(ObstacleName name, ObstacleType type) {
      if (type == ObstacleType.Barrier) {
        _gameStateMachine.Enter<GameplayLoseState>();
      }
    }
  }
}