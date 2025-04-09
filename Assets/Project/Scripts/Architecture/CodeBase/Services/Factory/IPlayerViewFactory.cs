using Project.Scripts.Architecture.CodeBase.Gameplay.Player;

namespace Project.Scripts.Architecture.CodeBase.Services.Factory {
  public interface IPlayerViewFactory: IService{
    T Create<T>(PlayerViewType type) where T : PlayerView;
  }
}