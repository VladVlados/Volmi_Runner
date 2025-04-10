using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;
using Project.Scripts.Architecture.CodeBase.Services.Save;
using Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Meta {
  public interface IPlayerRewardHandler {
    void ProcessReward(ObstacleName obstacleName);

    void SaveMatchResult();

    public MatchResult Result { get; }
  }

  public class PlayerRewardHandler : IPlayerRewardHandler {
    private readonly IDataProvider _dataProvider;
    private readonly IPlayerCollisionHandler _playerCollisionHandler;
    private readonly IGlobalDataService _globalDataService;
    private readonly GameplayProgressPanel _gameplayProgressPanel;

    public PlayerRewardHandler(IPlayerCollisionHandler playerCollisionHandler, IGlobalDataService globalDataService, IDataProvider dataProvider , IUIManager uiManager) {
      _playerCollisionHandler = playerCollisionHandler;
      _globalDataService = globalDataService;
      _dataProvider = dataProvider;
      _gameplayProgressPanel = uiManager.GetPanel<GameplayProgressPanel>();
      _playerCollisionHandler.OnObstacleCollision += OnObstacleCollision;
    }

    public void ProcessReward(ObstacleName obstacleName) {
      Result.Info.AddReward(obstacleName);
      Result.TotalScore += _globalDataService.GetGlobalData<RewardConfigurations>().GetPointsByReward(obstacleName);
      _gameplayProgressPanel.UpdateView(Result);
    }

    public void SaveMatchResult() {
      _dataProvider.GetSaveData().GetData<PlayerProgressData>().AddedNewResult(Result);
    }

    public MatchResult Result { get; } = new();

    private void OnObstacleCollision(ObstacleName name, ObstacleType type) {
      if (type == ObstacleType.Reward) {
        ProcessReward(name);
      }
    }
  }
}