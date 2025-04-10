using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Gameplay.Player;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;
using Project.Scripts.Architecture.CodeBase.Services.Save;
using Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using Project.Scripts.Architecture.CodeBase.UI.LoadScreen;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay;

namespace Project.Scripts.Architecture.CodeBase.GameStates.States.Gameplay {
  public class GameplaySceneSetupState : State {
    private readonly GameplayProgressPanel _gameplayProgressPanel;
    private readonly ILoaderScreenService _loaderScreenService;
    private readonly IGlobalDataService _globalDataService;
    private readonly IDataProvider _dataProvider;

    private IPlayerViewProvider _playerViewProvider;
    private ILevelGenerator _levelGenerator;

    public GameplaySceneSetupState(IGlobalDataService globalDataService, IDataProvider dataProvider, ILoaderScreenService loaderScreenService, IUIManager uiManager) {
      _globalDataService = globalDataService;
      _dataProvider = dataProvider;
      _loaderScreenService = loaderScreenService;
      _gameplayProgressPanel = uiManager.GetPanel<GameplayProgressPanel>();
    }

    public override void Enter() {
      base.Enter();
      SceneResolve();

      ConfigurationType configuration = _dataProvider.GetSaveData().GetData<PlayerProgressData>().ConfigurationType;
      ConfigurationInfo configurationInfo = _globalDataService.GetGlobalData<LevelConfigurations>().GetConfiguration(configuration);

      _playerViewProvider.CreateActualPlayerView();
      _levelGenerator.GenerateLevel(configurationInfo);
      _gameplayProgressPanel.PrepareView(configurationInfo.TargetRewards);

      _loaderScreenService.HideIntro();
      _gameStateMachine.Enter<GameplayWaitingState>();
    }

    private void SceneResolve() {
      _playerViewProvider = _container.Resolve<IPlayerViewProvider>();
      _levelGenerator = _container.Resolve<ILevelGenerator>();
    }
  }
}