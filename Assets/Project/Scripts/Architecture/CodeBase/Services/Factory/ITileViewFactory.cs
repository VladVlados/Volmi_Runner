using Project.Scripts.Architecture.CodeBase.Gameplay.Level;

namespace Project.Scripts.Architecture.CodeBase.Services.Factory {
  public interface ITileViewFactory : IService{
    T Create<T>(TileViewType type) where T : TileView;
  }
}