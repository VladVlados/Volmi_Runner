using System;
using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Gameplay.Player;

namespace Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes {
  [Serializable]
  public class PlayerCustomizationData : PlayerSavedData {
    private PlayerViewType _selectedPlayerView;
    private TileViewType _tileView;

    public override void Setup(IDataSaver saver) { }

    public void SelectPlayerView(PlayerViewType view) {
      _selectedPlayerView = view;
      Save();
    }

    public void SelectTileView(TileViewType view) {
      _tileView = view;
      Save();
    }

    public PlayerViewType PlayerView {
      get {
        return _selectedPlayerView;
      }
    }
    
    public TileViewType TileViewType {
      get {
        return _tileView;
      }
    }
  }
}