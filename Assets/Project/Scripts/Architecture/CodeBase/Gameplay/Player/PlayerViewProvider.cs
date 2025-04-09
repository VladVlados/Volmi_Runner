using Project.Scripts.Architecture.CodeBase.Pool;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using Project.Scripts.Architecture.CodeBase.Services.Save;
using Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Player {
  public class PlayerViewProvider : IPlayerViewProvider {
    private readonly IDataProvider _dataProvider;
    private readonly PlayerViewPool _playerViewPool;
    
    private PlayerViewType _selectedViewType;
    private PlayerView _currentView;

    public PlayerViewProvider(IPlayerViewFactory gameFactory, IDataProvider dataProvider) {
      _playerViewPool = new PlayerViewPool(() => gameFactory.Create<PlayerView>(_selectedViewType));

      _dataProvider = dataProvider;
    }

    public void CreateActualPlayerView() {
      ActualizePlayerViewData();
      _currentView = _playerViewPool.Get();
    }

    public void ClearPlayerView() {
      _currentView.Return();
    }

    private void ActualizePlayerViewData() {
      _selectedViewType = _dataProvider.GetSaveData().GetData<PlayerCustomizationData>().PlayerView;
    }
  }

  public interface IPlayerViewProvider {
    public void CreateActualPlayerView();
    void ClearPlayerView();
  }
}